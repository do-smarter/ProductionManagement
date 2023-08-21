using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Production;
using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.UniteProductionGroupsPriority
{
    public class UniteProductionGroupsPriorityCommandHandler : IRequestHandler<UniteProductionGroupsPriorityCommand, ProductionGroupVm>
    {
        private readonly IProductionGroupRepository _productionGroupRepository;
        private readonly IArchiveProductionGroupRepository _archiveProductionGroupRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public UniteProductionGroupsPriorityCommandHandler(IProductionGroupRepository productionGroupRepository,
                                                           IArchiveProductionGroupRepository archiveProductionGroupRepository,
                                                           IMapper mapper,
                                                           ProductionService productionService)
        {
            _productionGroupRepository = productionGroupRepository;
            _archiveProductionGroupRepository = archiveProductionGroupRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<ProductionGroupVm> Handle(UniteProductionGroupsPriorityCommand request, CancellationToken cancellationToken)
        {
            var validator = new UniteProductionGroupsPriorityCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            HashSet<Guid> uniqeGroups = new HashSet<Guid>(request.ProductionGroupIds);

            List<ProductionGroup> productionGroups = await _productionGroupRepository.FindListOfGroupsByIds(uniqeGroups);

            if (productionGroups.Count != uniqeGroups.Count)
            {
                string ids = String.Join(", or ", uniqeGroups.ToArray());
                throw new ResourceNotFoundException(nameof(ProductionGroup), ids);
            }


            ProductionGroup result = new ProductionGroup();
            try
            {
                await _productionGroupRepository.DeleteRangeProductionGroups(productionGroups);

                result.Priority = await _productionService.AddSingleProductionGroupPriority(result, request.Priority);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionGroup),
                                                    string.Join(", ", request.ProductionGroupIds.ToArray()));
            }
            var mergedProductionItems = MergeProductionItems(productionGroups, request.UserName);

            result.ProductionItems = mergedProductionItems[0];
            result.CreatedBy = request.UserName;
            result.LastModifiedBy = request.UserName;

            var histories = _productionService.PrepareProductionGroupHistoreis(productionGroups,
                                                                                           request.UserName,
                                                                                           ArchiveState.United);
            var x = 1;
            try
            {
                await _productionGroupRepository.AddAsync(result);
                await _archiveProductionGroupRepository.ArchiveRangeProductionGroup(histories);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionGroup),
                                                    string.Join(", ", request.ProductionGroupIds.ToArray()));
            }


            return _mapper.Map<ProductionGroupVm>(result);
        }
        public List<ProductionItem>[] MergeProductionItems(List<ProductionGroup> productionGroups, string UserName)
        {
            List<ProductionItem> allProdctionItems = new List<ProductionItem>();

            productionGroups.ForEach(g => allProdctionItems.AddRange(g.ProductionItems));

            var mergedProductionItems = new List<ProductionItem>();

            int i = allProdctionItems.Count - 1;
            while (i > 0)
            {
                for (int j = i - 1; j >= 0; j--)
                {

                    if (allProdctionItems[i].EqualsForProductionGroup(allProdctionItems[j]))
                    {
                        allProdctionItems[i] = new ProductionItem(allProdctionItems[i]);
                        allProdctionItems[i].Comment = $"{allProdctionItems[i].Comment}, {allProdctionItems[j].Comment}.";
                        allProdctionItems[i].OrderNumber = $"{allProdctionItems[i].OrderNumber}, {allProdctionItems[j].OrderNumber}.";
                        allProdctionItems[i].Quantity += allProdctionItems[j].Quantity;
                        allProdctionItems[i].LastModifiedBy = UserName;
                        allProdctionItems[i].CreatedBy = UserName;
                        mergedProductionItems.Add(allProdctionItems[i - 1]);
                        allProdctionItems.RemoveAt(j);
                        i--;
                    }
                }
                i--;
            }
            List<ProductionItem>[] result = { allProdctionItems, mergedProductionItems };

            return result;
        }

    }
}
