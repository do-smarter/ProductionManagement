using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities

{
    public class ProductionGroup : AuditableEntity
    {
        public Guid Id { get; set; } = new Guid();
        public List<ProductionItem> ProductionItems { get; set; } = new List<ProductionItem>();
        public bool IsMerged { get; set; } = false;
        public int Priority { get; set; }
    }

}
