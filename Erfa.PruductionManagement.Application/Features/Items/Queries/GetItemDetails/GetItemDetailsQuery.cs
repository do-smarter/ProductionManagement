using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemDetails
{
    public class GetItemDetailsQuery : IRequest<ItemVm>
    {
        public string ProductNumber { get; internal set; } = string.Empty;
        public GetItemDetailsQuery()
        {

        }

        public GetItemDetailsQuery(string productNumber)
        {
            ProductNumber = productNumber;
        }
    }
}
