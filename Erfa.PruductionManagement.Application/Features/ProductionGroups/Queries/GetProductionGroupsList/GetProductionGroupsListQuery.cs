using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Queries.GetProductionGroupsList
{
    public class GetProductionGroupsListQuery : IRequest<List<ProductionGroupVm>>
    {
    }
}
