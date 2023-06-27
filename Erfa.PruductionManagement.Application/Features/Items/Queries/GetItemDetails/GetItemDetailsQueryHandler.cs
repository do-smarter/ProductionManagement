using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemDetails
{
    internal class GetItemDetailsQueryHandler : IRequestHandler<GetItemDetailsQuery, ItemVm>
    {

        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public GetItemDetailsQueryHandler(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
        public async Task<ItemVm> Handle(GetItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetItemDetailsQueryValidator();
            await ProductionService.ValidateRequest(request, validator);

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }

            return _mapper.Map<ItemVm>(item);
        }
    }
}
