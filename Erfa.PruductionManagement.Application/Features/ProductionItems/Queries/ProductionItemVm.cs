using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Queries
{
    public class ProductionItemVm
    {

        public string ProductNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
