using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance
{
    public class ErfaArchiveDbContext : DbContext
    {
        public ErfaArchiveDbContext(DbContextOptions<ErfaArchiveDbContext> options) : base(options)
        {

        }

        public DbSet<ItemHistory> ArchivedItems { get; set; }
        public DbSet<ProductionItemHistory> ArchivedProductionItems { get; set; }
        public DbSet<ProductionGroupHistory> ArchivedProductionGroups { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<ArchivedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ArchiveDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
