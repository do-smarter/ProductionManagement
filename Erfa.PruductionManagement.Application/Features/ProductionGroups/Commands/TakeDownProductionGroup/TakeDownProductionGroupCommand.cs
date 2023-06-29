using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.TakeDownProductionGroup
{
    public class TakeDownProductionGroupCommand : IRequest
    {
        public string UserName { get; set; } = string.Empty;
        public List<Guid> ProductionGroupIds { get; set; }
        public TakeDownProductionGroupCommand(List<Guid> request, string userName)
        {
            UserName = userName;
            ProductionGroupIds = request;
        }
    }
}
