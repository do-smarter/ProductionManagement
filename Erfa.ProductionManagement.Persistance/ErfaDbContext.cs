using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Erfa.ProductionManagement.Persistance
{
    public class ErfaDbContext : DbContext
    {
        public ErfaDbContext(DbContextOptions<ErfaDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ProductionItem> ProductionItems { get; set; }
        public DbSet<ProductionGroup> ProductionGroups { get; set; }
        public DbSet<ItemHistory> ArchivedItems { get; set; }
        public DbSet<ProductionItemHistory> ArchivedProductionItems { get; set; }
        public DbSet<ProductionGroupHistory> ArchivedProductionGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ErfaDbContext).Assembly);
            string category = "Shelv";
            string user = "Magdalena";

            Item i1 = new Item()
            {
                Category = category,
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                Description = "Very nice piece of metal",
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                ProductionTimeSec = 100,
                ProductNumber = "XYZ123",
                ProductWeight = 100,
            };

            Item i2 = new Item()
            {
                Category = category,
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                Description = "Not so nice piece of metal",
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                ProductionTimeSec = 50,
                ProductNumber = "ABC987",
                ProductWeight = 50,
            };

            ProductionItem pi1 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = i1,
                Comment = "Make it streight",
                OrderNumber = "o-156",
                Quantity = 13,
                RalGalv = "ancp"
            };

            ProductionItem pi2 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = i1,
                Comment = "Don't mix them",
                OrderNumber = "g-784",
                Quantity = 27,
                RalGalv = "xcx/ass"
            };
            ProductionItem pi3 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = i2,
                Comment = "",
                OrderNumber = "y-984",
                Quantity = 14,
                RalGalv = ""
            };

            ProductionItem pi4 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = i2,
                Comment = "ALl",
                OrderNumber = "yu-78",
                Quantity = 61,
                RalGalv = "opo-yp"
            };

            ProductionItem pi5 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = i2,
                Comment = "",
                OrderNumber = "y-984",
                Quantity = 14,
                RalGalv = ""
            };


            ProductionGroup pc1 = new ProductionGroup()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                ProductionItems = { pi1 }

            };

            ProductionGroup pc2 = new ProductionGroup()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                ProductionItems = { pi2, pi3 }
            };

            ProductionGroup pc3 = new ProductionGroup()
            {
                CreatedBy = user,
                CreatedDate = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.Now,
                Id = Guid.NewGuid(),
                ProductionItems = { pi4, pi5 }
            };
            List<ProductionGroup> prodList = new List<ProductionGroup> { pc1, pc2, pc3 };

            modelBuilder.Entity<Item>().HasData(i1, i2);

            modelBuilder.Entity<Item>().HasKey("ProductNumber");
        }

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
