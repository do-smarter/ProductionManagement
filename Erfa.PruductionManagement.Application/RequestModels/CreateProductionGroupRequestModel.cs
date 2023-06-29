using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class CreateProductionGroupRequestModel
    {
        public List<ProductionItemModel> ProductionItems = new List<ProductionItemModel>();
    }
}
