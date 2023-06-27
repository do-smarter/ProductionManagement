using Erfa.PruductionManagement.Application.Features.ProductionItems;
using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups
{
    public class ProductionGroupVm
    {
        public Guid Id { get; set; }
        public List<ProductionItemVm> ProductionItems { get; set; } = new List<ProductionItemVm>();
        public int Priority { get; set; }
        public string GroupState { get; set; } = string.Empty;
    }
}
