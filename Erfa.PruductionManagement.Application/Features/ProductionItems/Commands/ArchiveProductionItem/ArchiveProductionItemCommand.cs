using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ArchiveProductionItem
{
    public class ArchiveProductionItemCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
