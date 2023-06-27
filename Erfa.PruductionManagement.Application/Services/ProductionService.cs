using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Services
{
    public class ProductionService
    {
        private readonly IProductionGroupRepository _groupRepository;

        public ProductionService(IProductionGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        internal async Task<int> MergePriorities(ProductionGroup created, List<ProductionGroup> components)
        {
            
            List<ProductionGroup> groups = await _groupRepository.ListAllOrderByPriority();
            
            components.ForEach(c => {
                // TOFDO Add method for archiving production items and same for production groups and then call those methods here
                //    groups.Remove(c); 
                _groupRepository.DeleteAsync(c);


            });
            if (created.Priority>groups.Count)
            {
                created.Priority = groups.Count;
            }

            groups.ForEach(c => { c.Priority = groups.IndexOf(c) + 1; });
            groups.Insert(created.Priority, created);
            groups.ForEach(c => { c.Priority = groups.IndexOf(c) + 1; });

            return await _groupRepository.UpdateRangeAsync(groups);
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

        internal bool EqalProductItems(List<ProductionItem> productionItems)
        {
            if (productionItems.Count == 0) { return false; }
            foreach (var productionItem in productionItems)
            {
                if (!productionItems[0].ProdEquals(productionItem)) { return false; }
            }
            return true;
        }

        internal static string ProductionStatesListValidationMsg()
        {
            var stateList = Enum.GetValues(typeof(ProductionState)).Cast<ProductionState>().ToList();

            string message = "{PropertyName} must be one of:";
            foreach (var state in stateList)
            {
                message += " " + state.ToString();

            }
            return message;
        }

        internal async static Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator)
        {
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }
            return true;
        }
    }
}
