using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState
{
    public class ChangeProductionItemStateCommand : ChangeProductionItemStateRequestModel, IRequest
    {
        public string UserName { get; set; } = string.Empty;

        public ChangeProductionItemStateCommand(ChangeProductionItemStateRequestModel request, string userName)
        {
            UserName = userName;
            ProductionItemId = request.ProductionItemId;
            State = request.State;
        }
    }
}
