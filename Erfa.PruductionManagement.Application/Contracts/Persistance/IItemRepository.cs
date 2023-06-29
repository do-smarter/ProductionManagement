using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IItemRepository : IAsyncRepository<Item>
    {
        Task<Item> GetByProductNumber(string ProductNumber);
    }
}
