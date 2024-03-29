﻿using Erfa.PruductionManagement.Domain.Entities.Production;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IProductionItemRepository : IAsyncRepository<ProductionItem>
    {
        Task<ProductionItem> GetProductionItemWithItems(Guid id);
        Task<List<ProductionItem>> ListAllProductionItemsWithItems();
    }
}
