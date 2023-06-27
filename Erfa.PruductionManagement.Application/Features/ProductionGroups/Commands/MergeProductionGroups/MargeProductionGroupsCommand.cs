using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommand : IRequest<ProductionGroupVm>
    {
        public List<Guid> Groups { get; set; } = new List<Guid>();
    }
}
