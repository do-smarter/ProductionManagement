using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState
{
    public class ChangeProductionItemStateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string State { get; set; } = string.Empty;
    }
}
