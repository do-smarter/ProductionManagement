using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommand : MergeProductionGroupsRequestModel, IRequest<ProductionGroupVm>
    {
        public string UserName { get; set; } = string.Empty;
        public MargeProductionGroupsCommand(MergeProductionGroupsRequestModel request, string userName)
        {
            UserName = userName;
            ProductionGroupIds = request.ProductionGroupIds;
        }
    }

}
