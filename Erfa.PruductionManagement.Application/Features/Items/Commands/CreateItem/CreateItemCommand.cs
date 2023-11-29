using Erfa.PruductionManagement.Api.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommand : CreateItemRequestModel, IRequest<string>
    {
        public string UserName { get; set; } = string.Empty;
        public CreateItemCommand(CreateItemRequestModel request, string userName)
        {
            UserName = userName;
            ProductNumber = request.ProductNumber;
            Description = request.Description;
            ProductionTimeSec = request.ProductionTimeSec;
            MaterialProductName = request.MaterialProductName;
        }
    }
}
