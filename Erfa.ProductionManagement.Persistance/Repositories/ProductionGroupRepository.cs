using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{
    public class ProductionGroupRepository : BaseRepository<ProductionGroup>, IProductionGroupRepository
    {
        public ProductionGroupRepository(ErfaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductionGroup> MergeGroup(ProductionGroup resultProductionGroup,
                                                         List<ProductionGroup> mergedProductionGroups,
                                                         List<ProductionGroupHistory> mergedProductionGroupsHistories)

        {
            _dbContext.ProductionGroups.Add(resultProductionGroup);
            _dbContext.ProductionGroups.RemoveRange(mergedProductionGroups);
            foreach (var productionGroup in mergedProductionGroups)
            {
                _dbContext.ProductionItems.RemoveRange(productionGroup.ProductionItems);
            }

            _dbContext.ArchivedProductionGroupss.Include(productionGroup => productionGroup.ProductionItems);
            _dbContext.ArchivedProductionGroupss.AddRange(mergedProductionGroupsHistories);
            
            await _dbContext.SaveChangesAsync();

            return resultProductionGroup;

        }

        public async Task<ProductionGroup> FindGroupWithLowestPriority()
        {
            var group = await _dbContext.ProductionGroups
                                        .OrderByDescending(g => g.Priority)
                                        .FirstOrDefaultAsync();
            return group;
        }

        public async Task<List<ProductionGroup>> FindListOfGroupsByIds(HashSet<Guid> ids)
        {
            var groups = await _dbContext.ProductionGroups
                                         .Where(e => ids.Contains(e.Id))
                                         .Include(e => e.ProductionItems)
                                         .ThenInclude(e => e.Item)
                                         .OrderBy(e => e.Priority)
                                         .ToListAsync();
            return groups;
        }

        public async Task<List<ProductionGroup>> ListAllGroupsOrderedByPriority()
        {
            var groups = await _dbContext.ProductionGroups
                                         .Include(e => e.ProductionItems)
                                         .ThenInclude(e => e.Item)
                                         .OrderBy(g => g.Priority)
                                         .ToListAsync();
            return groups;
        }
    }
}
