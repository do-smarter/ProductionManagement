
using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
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
                throw new Exceptions.ValidationException(validationResults);
            }

            throw new NotImplementedException();
        }
    }
}
