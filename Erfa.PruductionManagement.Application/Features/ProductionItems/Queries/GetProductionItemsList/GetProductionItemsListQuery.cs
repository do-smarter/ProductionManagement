using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Queries
{
    public class GetProductionItemsListQuery : IRequest<List<ProductionItemVm>>
    {       
    }
}
