using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Domain.Common
{
    public class ArchivedEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ArchivedBy { get; set; }
        public DateTime ArchiveDate { get; set; } = new DateTime();
        public ArchiveState ArchiveState { get; set; }
        
    }
}
