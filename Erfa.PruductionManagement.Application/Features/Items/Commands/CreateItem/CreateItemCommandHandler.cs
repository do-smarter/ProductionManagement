using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, string>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAsyncRepository<ItemHistory> _itemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;
        private ILogger<CreateItemCommandHandler> _logger { get; }


        public CreateItemCommandHandler(IAsyncRepository<Item> itemRepository,
                                        IAsyncRepository<ItemHistory> itemHistoryRepository,
                                        IMapper mapper,
                                        ILogger<CreateItemCommandHandler> logger, ProductionService productionService)
        {
            _itemRepository = itemRepository;
            _itemHistoryRepository = itemHistoryRepository;
            _mapper = mapper;
            _logger = logger;
            _productionService = productionService;
        }

        public async Task<string> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = _mapper.Map<Item>(request);
            item.CreatedBy = request.UserName;

            ItemHistory history = _mapper.Map<ItemHistory>(item);
            history.ArchivedBy = request.UserName;
            history.ArchiveState = Domain.Enums.ArchiveState.Created;
            try
            {
                await _itemRepository.AddAsync(item);
                await _itemHistoryRepository.AddAsync(history);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new PersistanceFailedException(nameof(Item), request);
            }
            return item.ProductNumber;

        }
    }
}
