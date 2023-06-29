using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.CreateProductionGroup
{
    public class CreateProductionGroupCommand : IRequest<ProductionGroupVm>
    {
        public string UserName { get; set; } = string.Empty;
        public List<ProductionItemModel> ProductionItems = new List<ProductionItemModel>();

        public CreateProductionGroupCommand(List<ProductionItemModel> request, string userName)
        {
            UserName = userName;
            ProductionItems = request;
        }
    }
}
