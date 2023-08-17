using Erfa.PruductionManagement.Domain.Entities.Production;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IProductionGroupRepository : IAsyncRepository<ProductionGroup>
    {
        Task<ProductionGroup> FindGroupWithLowestPriority();
        Task<List<ProductionGroup>> FindListOfGroupsByIds(HashSet<Guid> ids);
        Task<List<ProductionGroup>> ListAllGroupsOrderedByPriority();
        Task<ProductionGroup> MergeGroup(ProductionGroup resultProductionGroup,
                                         List<ProductionGroup> mergedProductionGroups);
        Task<ProductionGroup> AddProductionGroupWithProductionItems(ProductionGroup entiy);
        Task<List<ProductionGroup>> AddRangeProductionGroupWithProductionItems(List<ProductionGroup> entities);
        Task DeleteRangeProductionGroups(List<ProductionGroup> productionGroups);
    }

}
