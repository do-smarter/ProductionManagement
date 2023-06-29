using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem
{
    public class EditProductionItemCommandHandler : IRequestHandler<EditProductionItemCommand>
    {
        private readonly IProductionItemRepository _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public EditProductionItemCommandHandler(IProductionItemRepository productionItemRepository,
                                                IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                                                IMapper mapper,
                                                ProductionService productionService)
        {
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(EditProductionItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new EditProductionItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            ProductionItem productionItem = await _productionItemRepository
                                            .GetProductionItemWithItems(request.ProductionItemId);
            if (productionItem == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductionItemId);
            }
            UpdateProductionItemProperties(productionItem, request);
            try
            {
                await _productionService.ArchiveProductionItem(productionItem, request.UserName, ArchiveState.Changed);
                await _productionItemRepository.UpdateAsync(productionItem);
            }
            catch 
            {
                throw new PersistanceFailedException(nameof(ProductionItem), request.ProductionItemId);
            }
            return Unit.Value;

        }
        private ProductionItem UpdateProductionItemProperties(ProductionItem productionItem, EditProductionItemCommand request)
        {
            productionItem.OrderNumber = request.OrderNumber;
            productionItem.Comment = request.Comment;
            productionItem.Quantity = request.Quantity;
            productionItem.RalGalv = request.RalGalv;
            productionItem.LastModifiedBy = request.UserName;
            return productionItem;
        }
    }

}
