namespace Erfa.PruductionManagement.Application.Features.Items
{
    public class ItemVm
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
