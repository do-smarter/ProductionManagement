using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem
{
    public class EditProductionItemCommand : EditProductionItemRequestModel, IRequest
    {
        public string UserName { get; set; } = string.Empty;

        public EditProductionItemCommand(EditProductionItemRequestModel request, string userName)
        {
            UserName = userName;
            ProductionItemId = request.ProductionItemId;
            Quantity = request.Quantity;
            OrderNumber = request.OrderNumber;
            RalGalv = request.RalGalv;
            Comment = request.Comment;
        }

    }
}
