namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class MergeProductionGroupsRequestModel
    {
        public List<Guid> ProductionGroupIds { get; set; } = new List<Guid>();
    }
}
