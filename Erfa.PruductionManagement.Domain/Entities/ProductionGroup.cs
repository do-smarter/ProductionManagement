using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities

{
    public class ProductionGroup : AuditableEntity
    {
        public Guid Id { get; set; } = new Guid();
        public List<ProductionItem> ProductionItems { get; set; } = new List<ProductionItem>();
        //public List<string> OrderNumbers { get; set; } = new List<string>();
        public bool IsMerged { get; set; }
        public int Priority { get; set; }
    }

}
