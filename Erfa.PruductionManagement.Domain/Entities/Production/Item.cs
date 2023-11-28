using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities.Production

{
    public class Item : AuditableEntity
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public bool Updated(object? obj)
        {
            return obj is Item item &&
                   ProductNumber == item.ProductNumber &&
                   (Description != item.Description ||
                   ProductionTimeSec != item.ProductionTimeSec ||
                   MaterialProductName != item.MaterialProductName ||
                   Category != item.Category);
        }
    }


}
