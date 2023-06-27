using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IProductionGroupRepository : IAsyncRepository<ProductionGroup>
    {
        Task<ProductionGroup> FindGroupWithLowestPriority();
        Task<List<ProductionGroup>> FindListOfGroupsByIds(HashSet<Guid> ids);
        Task<List<ProductionGroup>> ListAllOrderByPriority();

    }
}
