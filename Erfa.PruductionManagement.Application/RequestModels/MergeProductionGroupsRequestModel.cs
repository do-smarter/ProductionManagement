namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class MergeProductionGroupsRequestModel
    {
        public List<Guid> Groups { get; set; } = new List<Guid>();
    }
}
