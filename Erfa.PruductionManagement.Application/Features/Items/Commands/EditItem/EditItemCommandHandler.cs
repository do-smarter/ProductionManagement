
using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IAsyncRepository<ItemHistory> _itemHistoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public EditItemCommandHandler(IAsyncRepository<ItemHistory> itemHistoryRepository,
                                        IItemRepository itemRepository,
                                        IMapper mapper,
                                        ProductionService productionService)
        {
            _itemHistoryRepository = itemHistoryRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            Item updated = _mapper.Map<Item>(request);
            updated.LastModifiedBy = request.UserName;
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }
            if (!item.Updated(updated))
            {
                throw new EntityUpdateException(nameof(Item), request.ProductNumber);
            }

            ItemHistory history = _mapper.Map<ItemHistory>(item);
            history.ArchivedBy = request.UserName;
            history.ArchiveState = Domain.Enums.ArchiveState.Changed;

            item = UpdateItemProperties(item, request);

            try
            {
                await _itemHistoryRepository.AddAsync(history);
                await _itemRepository.UpdateAsync(item);
            }
            catch
            {
                throw new PersistanceFailedException(nameof(Item), request.ProductNumber);
            }

            return Unit.Value;
        }

        public Item UpdateItemProperties(Item item, EditItemCommand command)
        {
            item.Category = command.Category;
            item.Description = command.Description;
            item.ProductWeight = command.ProductWeight;
            item.ProductionTimeSec = command.ProductionTimeSec;
            return item;
        }
    }
}
