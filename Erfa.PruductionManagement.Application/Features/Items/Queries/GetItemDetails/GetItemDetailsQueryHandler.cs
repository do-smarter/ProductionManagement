using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Production;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemDetails
{
    internal class GetItemDetailsQueryHandler : IRequestHandler<GetItemDetailsQuery, ItemVm>
    {

        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;
        public GetItemDetailsQueryHandler(IMapper mapper, IItemRepository itemRepository,
                                          ProductionService productionService)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _productionService = productionService;
        }
        public async Task<ItemVm> Handle(GetItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetItemDetailsQueryValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }
            return _mapper.Map<ItemVm>(item);
        }
    }
}
