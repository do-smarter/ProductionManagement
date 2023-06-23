using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem
{
    public class ArchiveItemCommand : IRequest
    {
        public string ProductNumber { get; set; } = string.Empty;
    }
}
