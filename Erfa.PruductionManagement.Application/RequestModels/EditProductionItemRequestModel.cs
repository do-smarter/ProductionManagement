namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class EditProductionItemRequestModel
    {
        public Guid ProductionItemId { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
