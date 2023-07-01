using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.UniteProductionGroupsPriority
{
    public class UniteProductionGroupsPriorityCommand : UniteProductionGroupsPriorityRequestModel, IRequest<ProductionGroupVm>
    {
        public string UserName { get; set; } = string.Empty;

        public UniteProductionGroupsPriorityCommand(UniteProductionGroupsPriorityRequestModel request, string userName)
        {
            UserName = userName;
            Priority = request.Priority;
            ProductionGroupIds = request.ProductionGroupIds;
        }
    }
}
