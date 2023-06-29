using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;
using System.Text.RegularExpressions;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommand : MergeProductionGroupsRequestModel, IRequest<ProductionGroupVm>
    {
        public string UserName { get; set; } = string.Empty;
        public MargeProductionGroupsCommand(MergeProductionGroupsRequestModel request, string userName)
        {
            UserName = userName;
            Groups = request.Groups;
        }
    }

}
