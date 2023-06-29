using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem
{
    public class EditProductionItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
