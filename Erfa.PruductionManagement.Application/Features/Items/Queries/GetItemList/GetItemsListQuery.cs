using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList
{
    public class GetItemsListQuery : IRequest<List<ItemVm>>
    {
    }
}