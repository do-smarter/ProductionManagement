using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState
{
    public class ChangeProductionItemStateCommandHandler : IRequestHandler<ChangeProductionItemStateCommand>
    {
        private readonly IProductionItemRepository _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;



        public ChangeProductionItemStateCommandHandler(
                         IProductionItemRepository productionItemRepository,
                         IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                         IMapper mapper,
                         ProductionService productionService)
        {
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(ChangeProductionItemStateCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeProductionItemStateCommandValidator(_productionService);
            await _productionService.ValidateRequest(request, validator);

            ProductionState state;
            if (!Enum.TryParse<ProductionState>(request.State, true, out state))
            {
                throw new EntityUpdateException(nameof(ProductionItem), request.ProductionItemId);
            }
            ProductionItem productionItem = await _productionItemRepository
                                            .GetProductionItemWithItems(request.ProductionItemId);
            if (productionItem == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductionItemId);
            }
            if (state.Equals(productionItem.State)) { return Unit.Value; }

            productionItem.State = state;
            ProductionItemHistory history = _mapper.Map<ProductionItemHistory>(productionItem);
            history.ArchivedBy = request.UserName;
            history.ArchiveState = ArchiveState.Changed;

            try
            {
                await _productionService.ArchiveProductionItem(productionItem, request.UserName, ArchiveState.Changed);
                await _productionItemRepository.UpdateAsync(productionItem);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionItem), request.ProductionItemId);
            }

            return Unit.Value;
        }
    }
}
