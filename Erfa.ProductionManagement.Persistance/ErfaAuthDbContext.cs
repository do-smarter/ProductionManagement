using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance
{
    public class ErfaAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public ErfaAuthDbContext(DbContextOptions<ErfaAuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.seedRoles(builder);
        }

        private void seedRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationIdentityRole>().HasData
                (
                new ApplicationIdentityRole() { Name = "ProdAdmin", ConcurrencyStamp = "1", NormalizedName = "ProdAdmin" },
                new ApplicationIdentityRole() { Name = "Worker", ConcurrencyStamp = "2", NormalizedName = "Worker" },
                new ApplicationIdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is AuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditableEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                }
                ((AuditableEntity)entry.Entity).LastModifiedDate = DateTime.UtcNow;
            }
        }
    }
}
