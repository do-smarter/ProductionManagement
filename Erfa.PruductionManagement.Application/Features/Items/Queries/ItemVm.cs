namespace Erfa.PruductionManagement.Application.Features.Items.Queries
{
    public class ItemVm
    {
        public Guid Id { get; set; }
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public double ProductWeight { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
