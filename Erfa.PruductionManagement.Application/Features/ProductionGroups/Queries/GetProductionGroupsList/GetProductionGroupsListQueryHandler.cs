using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Queries.GetProductionGroupsList
{
    public class GetProductionGroupsListQueryHandler : IRequestHandler<GetProductionGroupsListQuery, List<ProductionGroupVm>>
    {
        private readonly IProductionGroupRepository _productionGroupRepository;
        private readonly IMapper _mapper;

        public GetProductionGroupsListQueryHandler(IProductionGroupRepository productionGroupRepository, IMapper mapper)
        {
            _productionGroupRepository = productionGroupRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductionGroupVm>> Handle(GetProductionGroupsListQuery request, CancellationToken cancellationToken)
        {
            var allGroups = await _productionGroupRepository.ListAllGroupsOrderedByPriority();
            return _mapper.Map<List<ProductionGroupVm>>(allGroups);
        }
    }
}
