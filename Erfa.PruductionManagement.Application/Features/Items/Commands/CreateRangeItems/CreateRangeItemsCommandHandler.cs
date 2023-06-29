using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems
{
    public class CreateRangeItemsCommandHandler : IRequestHandler<CreateRangeItemsCommand, List<string>>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateItemCommandHandler> _logger;

        public CreateRangeItemsCommandHandler(IAsyncRepository<Item> itemRepository, IMapper mapper, ILogger<CreateItemCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<string>> Handle(CreateRangeItemsCommand request, CancellationToken cancellationToken)
        {
            List<string> result = new List<string>();
            var validator = new CreateRangeItemsCommandValidator();
            await ProductionService.ValidateRequest(request, validator);

            List<Item> items = _mapper.Map<List<Item>>(request.Items);
            foreach (var item in items)
            {
                item.CreatedBy = request.UserName;
                item.LastModifiedBy = request.UserName;
                result.Add(item.ProductNumber);
            }

            try
            {
                await _itemRepository.AddRangeAsync(items);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new PersistanceFailedException(nameof(Item), String.Join(", ", result.ToArray()));
            }
        }
    }
}

