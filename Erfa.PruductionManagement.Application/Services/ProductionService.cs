using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Services
{
    public class ProductionService
    {
        private readonly IProductionGroupRepository _groupRepository;
        private readonly IProductionItemRepository _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IArchiveProductionGroupRepository _archiveProductionGroupRepository;
        private readonly IMapper _mapper;

        public ProductionService(IProductionGroupRepository groupRepository,
                                 IProductionItemRepository productionItemRepository,
                                 IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                                 IArchiveProductionGroupRepository archiveProductionGroupRepository,
                                 IMapper mapper)
        {
            _groupRepository = groupRepository;
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _archiveProductionGroupRepository = archiveProductionGroupRepository;
            _mapper = mapper;
        }

        internal async Task MergePriorities(ProductionGroup created,
                                             List<ProductionGroup> mergedProductionGroups, string user)
        {
            List<ProductionGroupHistory> productionGroupHistories
                                = PrepareProductionGroupHistoreis(mergedProductionGroups, user, ArchiveState.Merged);

            try
            {
                await _groupRepository.MergeGroup(created, mergedProductionGroups);
                await _archiveProductionGroupRepository.ArchiveRangeProductionGroup(productionGroupHistories);
                await UpdatePriorities();

            }
            catch (Exception ex)
            {
                string ids = "";
                foreach (ProductionGroup group in mergedProductionGroups)
                {
                    ids += " - " + group.Id;

                }
                throw new PersistanceFailedException(nameof(ProductionGroup), ids);
            }
        }

        internal List<ProductionGroupHistory> PrepareProductionGroupHistoreis(List<ProductionGroup> productionGroups,
                                                                              string user,
                                                                              ArchiveState archiveState)
        {
            List<ProductionGroupHistory> productionGroupHistories = new List<ProductionGroupHistory>();
            foreach (var productionGroup in productionGroups)
            {
                ProductionGroupHistory productionGroupHistory = _mapper.Map<ProductionGroupHistory>(productionGroup);
                productionGroupHistory.ArchivedBy = user;
                productionGroupHistory.ArchiveState = archiveState;
                foreach (var productionItemHistory in productionGroupHistory.ProductionItems)
                {
                    productionItemHistory.ArchivedBy = user;
                    productionItemHistory.ArchiveState = archiveState;
                }
                productionGroupHistories.Add(productionGroupHistory);
            }
            return productionGroupHistories;
        }

        internal async Task<int> EstimateLowestPriority()
        {
            var group = await _groupRepository.FindGroupWithLowestPriority();
            if (group == null)
            {
                return 0;
            }
            return group.Priority;
        }

        internal async Task UpdatePriorities()
        {
            List<ProductionGroup> groups = await _groupRepository.ListAllGroupsOrderedByPriority();
            foreach (var group in groups)
            {
                group.Priority = groups.IndexOf(group) + 1;
            }

            await _groupRepository.UpdateRangeAsync(groups);
        }

        internal async Task<int> AddSingleProductionGroupPriority(ProductionGroup productionGroup, int priority)
        {
            List<ProductionGroup> groups = await _groupRepository.ListAllGroupsOrderedByPriority();
            var placeHolder = new ProductionGroup();
            if (priority < groups.Count)
            {
                groups.Insert(priority - 1, placeHolder);
                foreach (var group in groups)
                {
                    group.Priority = groups.IndexOf(group) + 1;
                }
            }
            else
            {
                groups.Add(placeHolder);
                productionGroup.Priority = groups.IndexOf(placeHolder) + 1;
                foreach (var group in groups)
                {
                    group.Priority = groups.IndexOf(group) + 1;
                }
            }
            groups.Remove(placeHolder);

            await _groupRepository.UpdateRangeAsync(groups);
            return placeHolder.Priority;
        }

        internal bool EqalProductItems(List<ProductionItem> productionItems)
        {
            if (productionItems.Count == 0) { return false; }
            foreach (var productionItem in productionItems)
            {
                if (!productionItems[0].EqualsForProductionGroup(productionItem)) { return false; }
            }
            return true;
        }

        internal string ProductionStatesListValidationMsg()
        {
            var stateList = Enum.GetValues(typeof(ProductionState)).Cast<ProductionState>().ToList();

            string message = "{PropertyName} must be one of:";
            foreach (var state in stateList)
            {
                message += " " + state.ToString();
            }
            return message;
        }

        internal async Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator)
        {
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }
            return true;
        }

        internal async Task<ProductionItemHistory> ArchiveProductionItem(ProductionItem item, string userName, ArchiveState archiveState)
        {
            ProductionItemHistory productionItemHistory = _mapper.Map<ProductionItemHistory>(item);
            productionItemHistory.ArchivedBy = userName;
            productionItemHistory.ArchiveState = archiveState;
            return await _productionItemHistoryRepository.AddAsync(productionItemHistory);
        }

        internal ProductionItem MergeProductionItems(List<ProductionItem> productionItems, string userName)
        {
            ProductionItem productionItem = new ProductionItem();
            productionItem.Item = productionItems.First().Item;
            productionItem.Quantity = productionItems.Sum(p => p.Quantity);
            productionItem.RalGalv = productionItems[0].RalGalv;

            HashSet<string> orders = new HashSet<string>();
            HashSet<string> comments = new HashSet<string>();
            foreach (ProductionItem item in productionItems)
            {
                orders.Add(item.OrderNumber);
                string comment = item.Comment;
                comments.Add(item.Comment);
            }
            productionItem.OrderNumber = String.Join(", ", orders);
            productionItem.Comment = String.Join(", ", comments);
            productionItem.CreatedBy = userName;
            productionItem.LastModifiedBy = userName;
            return productionItem;
        }
    }
}
