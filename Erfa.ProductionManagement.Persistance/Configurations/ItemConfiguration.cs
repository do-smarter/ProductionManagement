using Erfa.PruductionManagement.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erfa.ProductionManagement.Persistance.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.ProductNumber)
                .IsRequired()
               ;
        }
    }
}
