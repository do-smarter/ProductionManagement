using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IItemRepository : IAsyncRepository<Item>
    {
        public Task<Item> GetByProductNumber(string ProductNumber);
    }
}
