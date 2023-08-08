using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{
    public class ProductionGroupRepository : BaseRepository<ProductionGroup>, IProductionGroupRepository
    {
        public ProductionGroupRepository(ErfaProductionDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductionGroup> MergeGroup(ProductionGroup resultProductionGroup,
                                                         List<ProductionGroup> mergedProductionGroups)
        {
            _dbContext.ProductionGroups.Add(resultProductionGroup);
            _dbContext.ProductionGroups.RemoveRange(mergedProductionGroups);
            foreach (var productionGroup in mergedProductionGroups)
            {
                _dbContext.ProductionItems.RemoveRange(productionGroup.ProductionItems);
            }
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

        public async Task<ProductionGroup> AddProductionGroupWithProductionItems(ProductionGroup entiy)
        {
            _dbContext.ProductionGroups.Add(entiy);
            _dbContext.ProductionGroups.Include(entity => entity.ProductionItems);
            await _dbContext.SaveChangesAsync();
            return entiy;
        }

        public async Task<List<ProductionGroup>> AddRangeProductionGroupWithProductionItems(List<ProductionGroup> entities)
        {
            _dbContext.ProductionGroups.AddRange(entities);
            _dbContext.ProductionGroups.Include(entity => entity.ProductionItems);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteRangeProductionGroups(List<ProductionGroup> productionGroups)
        {
            productionGroups.ForEach(prod => _dbContext.RemoveRange(prod.ProductionItems));
            _dbContext.ProductionGroups.RemoveRange(productionGroups);

            await _dbContext.SaveChangesAsync();
        }
    }
}
