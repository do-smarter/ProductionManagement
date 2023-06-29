using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Domain.Entities

{
    public class ProductionGroup : AuditableEntity
    {
        public Guid Id { get; set; } = new Guid();
        public List<ProductionItem> ProductionItems { get; set; } = new List<ProductionItem>();
        public int Priority { get; set; }
    }

}
