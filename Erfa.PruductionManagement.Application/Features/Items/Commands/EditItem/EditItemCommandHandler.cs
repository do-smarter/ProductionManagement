
using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public EditItemCommandHandler(IItemRepository itemRepository,
                                        IMapper mapper,
                                        ProductionService productionService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);

            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }

            Item updated = _mapper.Map<Item>(request);
            if (!item.Updated(updated))
            {
                return Unit.Value;
            }

            item = UpdateItemProperties(item, request);

            try
            {
                await _itemRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(Item), request.ProductNumber);
            }

            return Unit.Value;
        }

        public Item UpdateItemProperties(Item item, EditItemCommand command)
        {
            item.Description = command.Description;
            item.MaterialProductName = command.MaterialProductName;
            item.ProductionTimeSec = command.ProductionTimeSec;
            item.LastModifiedBy = command.UserName;

            return item;
        }
    }
}
