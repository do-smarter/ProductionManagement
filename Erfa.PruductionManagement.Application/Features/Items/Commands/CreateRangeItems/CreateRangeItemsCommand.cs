using Erfa.PruductionManagement.Api.RequestModels;
using MediatR;

namespace Erfa.PruductionManagement.Application.RequestModels
{
    public class CreateRangeItemsCommand : IRequest<List<string>>
    {
        public string UserName { get; set; } = string.Empty;
        public List<CreateItemRequestModel> Items { get; set; } = new List<CreateItemRequestModel>();
        public CreateRangeItemsCommand(List<CreateItemRequestModel> request, string userName)
        {
            UserName = userName;
            Items = request;
        }
    }
}
