namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class ChangeProductionItemStateRequestModel
    {
        public Guid ProductionItemId { get; set; }
        public string State { get; set; } = string.Empty;
    }
}
