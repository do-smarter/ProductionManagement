using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    public class CreateRangeItemsCommand : List<CreateItemCommand>, IRequest<List<string>>
    {
    }
}
