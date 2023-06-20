using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries
{
    public class GetItemsListQuery : IRequest<List<ItemVm>>
    {
    }
}