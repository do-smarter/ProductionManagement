using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Domain.Entities
{
    public class ProductionItemHistory : ArchivedEntity
    {        
        public Guid  ProductionItemId {get; set; }
        public string ProductNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public ProductionState State { get; set; } 
    }
}
