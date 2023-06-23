using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IProductionGroupRepository : IAsyncRepository<ProductionGroup>
    {
        public Task<ProductionGroup> FindGroupWithLowestPriority();
    }
}
