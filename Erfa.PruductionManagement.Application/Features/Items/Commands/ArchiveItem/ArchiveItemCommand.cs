using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem
{
    public class ArchiveItemCommand : ArchiveItemRequestModel, IRequest
    {
        public string UserName { get; set; } = string.Empty;
        public ArchiveItemCommand(ArchiveItemRequestModel request, string userName)
        {
            UserName = userName;
            ProductNumber = request.ProductNumber;
        }
    }
}
