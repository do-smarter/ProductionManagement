using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{
    public class ProductionItemRepository : BaseRepository<ProductionItem>, IProductionItemRepository
    {
        public ProductionItemRepository(ErfaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductionItem> GetProductionItemWithItems(Guid id)
        {
            return await _dbContext.ProductionItems.Where(pi => pi.Id == id)
                .Include(pi => pi.Item).FirstOrDefaultAsync();
        }

        public async Task<List<ProductionItem>> ListAllProductionItemsWithItems()
        {
            return await _dbContext.ProductionItems.Where(x => 1 == 1)
                .Include(pi => pi.Item).ToListAsync();
        }
    }
}
