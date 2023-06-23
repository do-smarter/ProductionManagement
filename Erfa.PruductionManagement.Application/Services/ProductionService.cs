using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Services
{
    public class ProductionService
    {
        private readonly IProductionGroupRepository _groupRepository;

        public ProductionService(IProductionGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        private async Task<int> EstimateLowestPriority()
        {
            var group = await _groupRepository.FindGroupWithLowestPriority();
            if (group == null)
            {
                return 0;
            }
            return group.Priority;
        }

        internal async Task GroupProductionItemAsync(ProductionItem productionItem)
        {
            ProductionGroup group = new ProductionGroup();
            group.ProductionItems.Add(productionItem);
            group.Priority = await EstimateLowestPriority() + 1;

            await _groupRepository.AddAsync(group);            
        }
    }
}
