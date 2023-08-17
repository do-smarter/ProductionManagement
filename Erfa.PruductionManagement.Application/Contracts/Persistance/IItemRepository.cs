using Erfa.PruductionManagement.Domain.Entities.Production;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IItemRepository : IAsyncRepository<Item>
    {
        Task<Item> GetByProductNumber(string ProductNumber);
        Task<List<Item>> FindListOfItemsByProductNumbers(HashSet<string> productNumberds);

    }
}
