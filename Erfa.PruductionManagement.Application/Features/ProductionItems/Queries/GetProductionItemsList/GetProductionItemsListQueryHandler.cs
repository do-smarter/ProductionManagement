using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Queries.GetProductionItemsList
{
    public class GetProductionItemsListQueryHandler : IRequestHandler<GetProductionItemsListQuery, List<ProductionItemVm>>
    {
        private readonly IProductionItemRepository _productionItemRepository;
        private readonly IMapper _mapper;

        public GetProductionItemsListQueryHandler(
                         IProductionItemRepository productionItemRepository, 
                         IMapper mapper)
        {
            _productionItemRepository = productionItemRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductionItemVm>> Handle(GetProductionItemsListQuery request, CancellationToken cancellationToken)
        {
            var allPtoductionItems = (await _productionItemRepository.ListAllProductionItemsWithItems())
                                     .OrderBy(x => x.OrderNumber).ToList();
            return _mapper.Map<List<ProductionItemVm>>(allPtoductionItems);
        }
    }
}
