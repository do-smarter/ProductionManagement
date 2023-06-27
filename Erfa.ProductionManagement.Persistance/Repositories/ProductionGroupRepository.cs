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
                                         .OrderByDescending(e => e.Priority)
                                         .ToListAsync();
            return groups;
        }

        public async Task<List<ProductionGroup>> ListAllOrderByPriority()
        {
            var groups = await _dbContext.ProductionGroups
                                         .OrderByDescending(g => g.Priority)
                                         .ToListAsync();
            return groups;
        }
    }
}
