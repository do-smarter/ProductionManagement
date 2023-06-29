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
        private readonly IAsyncRepository<ProductionItem> _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IMapper _mapper;


        public ChangeProductionItemStateCommandHandler(
                         IAsyncRepository<ProductionItem> productionItemRepository,
                         IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                         IMapper mapper)
        {
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ChangeProductionItemStateCommand request, CancellationToken cancellationToken)
        {
            var validator = new ChangeProductionItemStateCommandValidator();
            await ProductionService.ValidateRequest(request, validator);

            ProductionState state;
            if (!Enum.TryParse<ProductionState>(request.State, out state))
            {
                throw new EntityUpdateException(nameof(ProductionItem), request.Id);
            }

            ProductionItem productionItem = await _productionItemRepository.GetByIdAsync(request.Id);
            if (productionItem == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.Id);
            }

            productionItem.State = state;

            ProductionItemHistory history = _mapper.Map<ProductionItemHistory>(productionItem);
            history.ArchivedBy = "Magdalena";
            history.ArchiveState = ArchiveState.Changed;

            try
            {
                await _productionItemHistoryRepository.AddAsync(history);
                await _productionItemRepository.DeleteAsync(productionItem);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionItem), request.Id);
            }

            throw new NotImplementedException();
        }
    }
}
