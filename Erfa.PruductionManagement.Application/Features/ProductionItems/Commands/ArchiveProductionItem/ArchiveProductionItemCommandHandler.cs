using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ArchiveProductionItem
{
    public class ArchiveProductionItemCommandHandler : IRequestHandler<ArchiveProductionItemCommand>
    {
        private readonly IAsyncRepository<ProductionItem> _productionItemRepository;
        private readonly IAsyncRepository<ProductionItemHistory> _productionItemHistoryRepository;
        private readonly IMapper _mapper;


        public ArchiveProductionItemCommandHandler(
                         IAsyncRepository<ProductionItem> productionItemRepository,
                         IAsyncRepository<ProductionItemHistory> productionItemHistoryRepository,
                         IMapper mapper)
        {
            _productionItemRepository = productionItemRepository;
            _productionItemHistoryRepository = productionItemHistoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ArchiveProductionItemCommand request,
                                       CancellationToken cancellationToken)
        {
            var validator = new ArchiveProductionItemCommandValidator();
            await ProductionService.ValidateRequest(request, validator);


            ProductionItem productionItem = await _productionItemRepository.GetByIdAsync(request.Id);
            if (productionItem == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.Id);
            }

            ProductionItemHistory history = _mapper.Map<ProductionItemHistory>(productionItem);
            history.ArchivedBy = "Magdalena";
            history.ArchiveState = ArchiveState.Archived;

            try
            {
                await _productionItemHistoryRepository.AddAsync(history);
                await _productionItemRepository.DeleteAsync(productionItem);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionItem), request.Id);
            }
            return Unit.Value;
        }
    }
}
