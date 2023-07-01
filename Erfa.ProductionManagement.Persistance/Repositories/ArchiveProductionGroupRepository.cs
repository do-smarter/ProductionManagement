using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance.Repositories
{
    public class ArchiveProductionGroupRepository : BaseRepository<ProductionGroupHistory>, IArchiveProductionGroupRepository
    {
        public ArchiveProductionGroupRepository(ErfaArchiveDbContext dbContext) : base(dbContext) { }
        public async Task<List<ProductionGroupHistory>> ArchiveRangeProductionGroup(List<ProductionGroupHistory> productionGroupHistories)
        {
            _dbContext.ArchivedProductionGroups.Include(productionGroupHistory => productionGroupHistory.ProductionItems);
            _dbContext.ArchivedProductionGroups.AddRange(productionGroupHistories);

            await _dbContext.SaveChangesAsync();

            return productionGroupHistories;
        }
    }
}
