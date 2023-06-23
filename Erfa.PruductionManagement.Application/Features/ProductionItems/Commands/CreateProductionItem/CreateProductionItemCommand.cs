using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem
{
    public class CreateProductionItemCommand : IRequest<Guid>
    {
        public string ProductNumber { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string State { get; set; } = ProductionState.New.ToString();
        
    }
}
