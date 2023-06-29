namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class TakeDownProductionGroupsRequestModel
    {
        public List<Guid> ProductionGroupIds { get; set; } = new List<Guid>();
    }
}
