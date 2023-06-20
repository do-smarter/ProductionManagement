using Erfa.PruductionManagement.Application.Features.Items.Queries;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    public class CreateItemCommand : IRequest<Guid>
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public double ProductWeight { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
