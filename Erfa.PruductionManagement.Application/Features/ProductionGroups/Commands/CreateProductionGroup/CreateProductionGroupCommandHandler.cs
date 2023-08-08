using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.CreateProductionGroup
{
    public class CreateProductionGroupCommandHandler : IRequestHandler<CreateProductionGroupCommand, ProductionGroupVm>
    {
        private readonly IAsyncRepository<ProductionItem> _productionItemRepository;
        private readonly IProductionGroupRepository _productionGroupRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IArchiveProductionGroupRepository _archiveProductionGroupRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public CreateProductionGroupCommandHandler(IAsyncRepository<ProductionItem> productionItemRepository,
                                                   IProductionGroupRepository productionGroupRepository,
                                                   IArchiveProductionGroupRepository archiveProductionGroupRepository,
                                                   IItemRepository itemRepository,
                                                   IMapper mapper, ProductionService productionService)
        {
            _productionItemRepository = productionItemRepository;
            _productionGroupRepository = productionGroupRepository;
            _itemRepository = itemRepository;
            _archiveProductionGroupRepository = archiveProductionGroupRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<ProductionGroupVm> Handle(CreateProductionGroupCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductionGroupCommandValidator();
            await _productionService.ValidateRequest(request, validator);
            var productNumbers = new HashSet<string>();
            foreach (ProductionItemModel model in request.ProductionItems)
            {
                productNumbers.Add(model.ProductNumber);
            }
            var items = await _itemRepository.FindListOfItemsByProductNumbers(productNumbers);

            if (items.Count != productNumbers.Count)
            {
                string ids = String.Join(", or ", productNumbers.ToArray());
                throw new ResourceNotFoundException(nameof(Item), ids);
            }

            ProductionGroup productionGroup = new ProductionGroup();
            productionGroup.CreatedBy = request.UserName;
            productionGroup.Priority = (await _productionService.EstimateLowestPriority()) + 1;
            productionGroup.LastModifiedBy = request.UserName;
            foreach (ProductionItemModel model in request.ProductionItems)
            {
                ProductionItem productionItem = _mapper.Map<ProductionItem>(model);
                Item i = items.Find(i => i.ProductNumber.Equals(model.ProductNumber.ToUpper()));
                productionItem.Item = i;
                productionItem.CreatedBy = request.UserName;
                productionItem.LastModifiedBy = request.UserName;
                productionGroup.ProductionItems.Add(productionItem);
            }

            try
            {
                await _productionGroupRepository.AddProductionGroupWithProductionItems(productionGroup);
                var histories = _productionService.PrepareProductionGroupHistoreis(new List<ProductionGroup>() { productionGroup }, request.UserName, ArchiveState.Created);
                await _archiveProductionGroupRepository.ArchiveRangeProductionGroup(histories);
            }
            catch (Exception ex) { throw new PersistanceFailedException(nameof(productionGroup), request); }

            return _mapper.Map<ProductionGroupVm>(productionGroup);
        }
    }
}
