using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{

    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(ErfaProductionDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Item>> FindListOfItemsByProductNumbers(HashSet<string> productNumbers)
        {
            var items = await _dbContext.Items
                                         .Where(e => productNumbers.Contains(e.ProductNumber))                                         
                                         .ToListAsync();
            return items;
        }

        public async Task<Item> GetByProductNumber(string ProductNumber)
        {
            return await _dbContext.Items
                 .Where(i => string
                    .Equals(i.ProductNumber, ProductNumber)
                  )
                 .FirstOrDefaultAsync();
        }
    }
}
