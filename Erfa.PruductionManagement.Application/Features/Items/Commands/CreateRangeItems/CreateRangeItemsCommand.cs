using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems
{
    public class CreateRangeItemsCommand : List<CreateItemCommand>, IRequest<List<string>>
    {
    }
}
