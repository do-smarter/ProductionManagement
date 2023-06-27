using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems
{
    public class ProductionItemVm
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
