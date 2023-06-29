using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.TakeDownProductionGroup
{
    public class TakeDownProductionGroupCommand : TakeDownProductionGroupsRequestModel, IRequest
    {
        public string UserName { get; set; } = string.Empty;
        
        public TakeDownProductionGroupCommand(TakeDownProductionGroupsRequestModel request, string userName)
        {
            UserName = userName;
            ProductionGroupIds = request.ProductionGroupIds;
        }
    }
}
