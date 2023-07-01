namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class UniteProductionGroupsPriorityRequestModel
    {
        public List<Guid> ProductionGroupIds { get; set; } = new List<Guid>();
        public int Priority { get; set; }
    }
}
