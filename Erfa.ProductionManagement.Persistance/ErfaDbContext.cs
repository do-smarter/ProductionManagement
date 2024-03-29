﻿using AutoMapper.Configuration;
using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;
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
            modelBuilder.Entity<Item>().HasKey("ProductNumber");
            base.OnModelCreating(modelBuilder);

            string user = "Magdalena";

            Item i1 = new Item()
            {
                CreatedBy = user,
                CreatedDate = DateTime.UtcNow,
                Description = "Very nice piece of metal",
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
                ProductionTimeSec = 100,
                ProductNumber = "XYZ123",
                MaterialProductName = "some material",
            };

            Item i2 = new Item()
            {
                CreatedBy = user,
                CreatedDate = DateTime.UtcNow,
                Description = "Not so nice piece of metal",
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
                ProductionTimeSec = 50,
                ProductNumber = "ABC987",
                MaterialProductName = "Some other material",
            };

            ProductionItem pi1 = new ProductionItem()
            {
                CreatedBy = user,
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
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
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
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
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
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
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
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
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
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
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProductionItems = { pi1 }

            };

            ProductionGroup pc2 = new ProductionGroup()
            {
                CreatedBy = user,
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProductionItems = { pi2, pi3 }
            };

            ProductionGroup pc3 = new ProductionGroup()
            {
                CreatedBy = user,
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = user,
                LastModifiedDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                ProductionItems = { pi4, pi5 }
            };
            List<ProductionGroup> prodList = new List<ProductionGroup> { pc1, pc2, pc3 };

            modelBuilder.Entity<Item>().HasData(i1, i2);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<ArchivedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ArchiveDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
