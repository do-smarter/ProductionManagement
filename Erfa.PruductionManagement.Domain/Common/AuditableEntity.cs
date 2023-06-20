namespace Erfa.PruductionManagement.Domain.Common
{
    public class AuditableEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
    }

}
