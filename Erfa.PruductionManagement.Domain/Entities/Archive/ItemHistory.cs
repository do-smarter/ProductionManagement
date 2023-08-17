using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities.Archive
{
    public class ItemHistory : ArchivedEntity
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public double ProductWeight { get; set; }
        public string Category { get; set; } = string.Empty;

    }
}
