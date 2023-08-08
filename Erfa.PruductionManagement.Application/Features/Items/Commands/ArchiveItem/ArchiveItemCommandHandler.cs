using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using MediatR;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem
{
    public class ArchiveItemCommandHandler : IRequestHandler<ArchiveItemCommand>
    {
        private readonly IAsyncRepository<ItemHistory> _itemHistoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;
        public ArchiveItemCommandHandler(
                         IAsyncRepository<ItemHistory> itemHistoryRepository,
                         IItemRepository itemRepository, IMapper mapper,
                         ProductionService productionService)
        {
            _itemHistoryRepository = itemHistoryRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(ArchiveItemCommand request,
                                       CancellationToken cancellationToken)
        {
            var validator = new ArchiveItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = await _itemRepository.GetByProductNumber(request.ProductNumber);
            if (item == null)
            {
                throw new ResourceNotFoundException(nameof(Item), request.ProductNumber);
            }

            ItemHistory history = _mapper.Map<ItemHistory>(item);
            history.ArchivedBy = request.UserName;
            history.ArchiveState = Domain.Enums.ArchiveState.Archived;

            try
            {
                await _itemHistoryRepository.AddAsync(history);
                await _itemRepository.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ItemHistory), request.ProductNumber);
            }
            return Unit.Value;
        }
    }
}
