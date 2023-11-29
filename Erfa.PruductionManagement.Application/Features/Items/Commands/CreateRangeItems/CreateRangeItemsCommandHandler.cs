using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems
{
    public class CreateRangeItemsCommandHandler : IRequestHandler<CreateRangeItemsCommand, List<string>>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAsyncRepository<ItemHistory> _itemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateItemCommandHandler> _logger;
        private readonly ProductionService _productionService;

        public CreateRangeItemsCommandHandler(IAsyncRepository<Item> itemRepository,
                                              IAsyncRepository<ItemHistory> itemHistoryRepository,
                                              IMapper mapper,
                                              ILogger<CreateItemCommandHandler> logger,
                                              ProductionService productionService)
        {
            _itemRepository = itemRepository;
            _itemHistoryRepository = itemHistoryRepository;
            _mapper = mapper;
            _logger = logger;
            _productionService = productionService;
        }
        public async Task<List<string>> Handle(CreateRangeItemsCommand request, CancellationToken cancellationToken)
        {
            List<string> result = new List<string>();
            var validator = new CreateRangeItemsCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            List<Item> items = _mapper.Map<List<Item>>(request.Items);
            foreach (var item in items)
            {
                item.CreatedBy = request.UserName;
                item.LastModifiedBy = request.UserName;
                result.Add(item.ProductNumber);
            }
            List<ItemHistory> histories = _mapper.Map<List<ItemHistory>>(items);
            foreach (var history in histories)
            {
                history.ArchivedBy = request.UserName;
                history.ArchiveState = Domain.Enums.ArchiveState.Created;
            }

            try
            {
                await _itemHistoryRepository.AddRangeAsync(histories);
                await _itemRepository.AddRangeAsync(items);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);

                var b = ex.InnerException.Message;
                if (b.StartsWith("23505"))
                {
                    throw new EntityAddException("Some Product Numbers already exist");
                }
                throw new PersistanceFailedException(nameof(Item), String.Join(", ", result.ToArray()));
            }
        }
    }
}

