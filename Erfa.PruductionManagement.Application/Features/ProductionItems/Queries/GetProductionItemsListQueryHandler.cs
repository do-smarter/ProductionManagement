using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Queries
{
    public class GetProductionItemsListQueryHandler : IRequestHandler<GetProductionItemsListQuery, List<ProductionItemVm>>
    {
        private readonly IAsyncRepository<ProductionItem> _productionItemRepository;
        private readonly IMapper _mapper;

        public GetProductionItemsListQueryHandler(IAsyncRepository<ProductionItem> productionItemRepository, IMapper mapper)
        {
            _productionItemRepository = productionItemRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductionItemVm>> Handle(GetProductionItemsListQuery request, CancellationToken cancellationToken)
        {
            var allPtoductionItems = (await _productionItemRepository.ListAllAsync()).OrderBy(x => x.OrderNumber).ToList();
            return _mapper.Map<List<ProductionItemVm>>(allPtoductionItems);
        }
    }
}
