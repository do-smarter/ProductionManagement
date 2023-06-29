using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem
{
    public class EditItemCommand : EditItemRequestModel, IRequest
    {
        public string UserName { get; set; } = string.Empty;
        public EditItemCommand(EditItemRequestModel request, string userName)
        {
            UserName = userName;
            ProductNumber = request.ProductNumber;
            Description = request.Description;
            ProductionTimeSec = request.ProductionTimeSec;
            ProductWeight = request.ProductWeight;
            Category = request.Category;
        }
    }
}
