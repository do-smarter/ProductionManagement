using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem
{
    public class CreateProductionItemCommandHandler : IRequestHandler<CreateProductionItemCommand, Guid>
    {
        private readonly IAsyncRepository<ProductionItem> _productionItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public CreateProductionItemCommandHandler(
                        IAsyncRepository<ProductionItem> productionItemRepository, 
                        IItemRepository itemRepository, 
                        IMapper mapper, 
                        ProductionService productionService)
        {
            _productionItemRepository = productionItemRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Guid> Handle(CreateProductionItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductionItemCommandValidator();
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new ValidationException(validationResults);
            }

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }

            ProductionItem productionItem = _mapper.Map<ProductionItem>(request);
            productionItem.Item = item;
                        
            try
            {
                await _productionItemRepository.AddAsync(productionItem);
                await _productionService.GroupProductionItemAsync(productionItem);
            }
            catch (Exception ex)
            {

                throw new PersistanceFailedException(nameof(Item), request);
            }

            return productionItem.Id;
        }
    }
}
