using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Services
{
    public class ProductionService
    {
        private readonly IProductionGroupRepository _groupRepository;
        private readonly IProductionItemRepository _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IAsyncRepository<ProductionGroupHistory> _productionGRoupHistoryRepository;
        private readonly IMapper _mapper;

        public ProductionService(IProductionGroupRepository groupRepository,
                                 IProductionItemRepository productionItemRepository,
                                 IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                                 IAsyncRepository<ProductionGroupHistory> productionGRoupHistoryRepository,
                                 IMapper mapper)
        {
            _groupRepository = groupRepository;
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _productionGRoupHistoryRepository = productionGRoupHistoryRepository;
            _mapper = mapper;
        }

        internal async Task MergePriorities(ProductionGroup created,
                                             List<ProductionGroup> mergedProductionGroups, string user)
        {
            List<ProductionGroupHistory> productionGroupHistories
                                = PrepareProductionGroupHistoreis(mergedProductionGroups, user);

            try
            {
                await _groupRepository.MergeGroup(created, mergedProductionGroups, productionGroupHistories);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionGroup), "Multiple Ids");
            }
        }

        internal List<ProductionGroupHistory> PrepareProductionGroupHistoreis(List<ProductionGroup> productionGroups, String user)
        {
            List<ProductionGroupHistory> productionGroupHistories = new List<ProductionGroupHistory>();
            foreach (var productionGroup in productionGroups)
            {
                ProductionGroupHistory productionGroupHistory = _mapper.Map<ProductionGroupHistory>(productionGroup);
                productionGroupHistory.ArchiveState = ArchiveState.Archived;
                productionGroupHistory.ArchivedBy = user;
                productionGroupHistories.Add(productionGroupHistory);
            }
            return productionGroupHistories;
        }

        private List<ProductionGroup> RegroupPriorities(ProductionGroup created, List<ProductionGroup> groups)
        {
            if (created.Priority > groups.Count)
            {
                created.Priority = groups.Count;
            }

            groups.ForEach(c => { c.Priority = groups.IndexOf(c) + 1; });
            groups.Insert(created.Priority, created);
            groups.ForEach(c => { c.Priority = groups.IndexOf(c) + 1; });

            return groups;
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
