using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities
{
    public class ProductionGroupHistory : ArchivedEntity
    {
        public Guid ProductionGroupId { get; set; }
        public List<ProductionItemHistory> ProductionItems { get; set; } = new List<ProductionItemHistory>();

    }
}
