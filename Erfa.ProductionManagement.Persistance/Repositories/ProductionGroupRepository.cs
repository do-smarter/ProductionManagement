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
            var group = await _dbContext.ProductionGroups.OrderByDescending(g => g.Priority).FirstOrDefaultAsync();
            return group;
        }
    }
}
