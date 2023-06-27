using Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem
{
    public class EditProductionItemCommandHandler : IRequestHandler<EditProductionItemCommand>
    {
        public Task<Unit> Handle(EditProductionItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public ProductionItem UpdateProductionItemProperties(ProductionItem productionItem, EditProductionItemCommand command)
        {
            productionItem.OrderNumber = command.OrderNumber;
            productionItem.Comment = command.Comment;
            productionItem.Quantity = command.Quantity;
            productionItem.RalGalv = command.RalGalv;
            return productionItem;
        }
    }

}
