using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{

    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(ErfaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Item> GetByProductNumber(string ProductNumber)
        {
            return await _dbContext.Items
                 .Where(i => string
                    .Equals(i.ProductNumber, ProductNumber)
                  )
                 .FirstOrDefaultAsync();
            throw new NotImplementedException();
        }
    }
}
