﻿
using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem
{
    public class EditItemCommandHandler : IRequestHandler<EditItemCommand>
    {
        private readonly IAsyncRepository<ItemHistory> _itemHistoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public EditItemCommandHandler(IAsyncRepository<ItemHistory> itemHistoryRepository,
                                        IItemRepository itemRepository,
                                        IMapper mapper)
        {
            _itemHistoryRepository = itemHistoryRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditItemCommandValidator();
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new ValidationException(validationResults);
            }

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            Item updated = _mapper.Map<Item>(request);
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }
            if (!item.Updated(updated))
            {
                throw new EntityUnmodifiedException(nameof(Item), request);
            }

            ItemHistory history = _mapper.Map<ItemHistory>(item);
            // TODO Set user on histroy object
            history.ArchivedBy = "Magdalena";
            history.State = Domain.Enums.ArchiveState.Changed;

            item = UpdateItemProperties(item, request);

            try
            {
                await _itemHistoryRepository.AddAsync(history);
                await _itemRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(Item), request);
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