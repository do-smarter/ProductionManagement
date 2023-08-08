using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.TakeDownProductionGroup
{
    public class TakeDownProductionGroupCommandHandler : IRequestHandler<TakeDownProductionGroupCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductionGroupRepository _productionGroupRepository;
        private readonly IArchiveProductionGroupRepository _archiveProductionGroupRepository;
        private readonly ProductionService _productionService;

        public TakeDownProductionGroupCommandHandler(IMapper mapper,
                                                     IProductionGroupRepository productionGroupRepository,
                                                     IArchiveProductionGroupRepository archiveProductionGroupRepository,
                                                     ProductionService productionService)
        {
            _mapper = mapper;
            _productionGroupRepository = productionGroupRepository;
            _archiveProductionGroupRepository = archiveProductionGroupRepository;
            _productionService = productionService;
        }

        public async Task<Unit> Handle(TakeDownProductionGroupCommand request, CancellationToken cancellationToken)
        {
            var validator = new TakeDownProductionGroupCommandValidator();
            await _productionService.ValidateRequest(request, validator);
            var uniqeIds = new HashSet<Guid>();
            foreach (Guid id in request.ProductionGroupIds)
            {
                uniqeIds.Add(id);
            }
            List<ProductionGroup> productionGroups = await _productionGroupRepository.FindListOfGroupsByIds(uniqeIds);
            if (productionGroups.Count != uniqeIds.Count)
            {
                throw new ResourceNotFoundException(nameof(ProductionGroup),
                                                    string.Join(", ", request.ProductionGroupIds.ToArray()));
            }
            foreach (ProductionGroup productionGroup in productionGroups)
            {
                foreach (ProductionItem productionItem in productionGroup.ProductionItems)
                {
                    if (!(productionItem.State.Equals(ProductionState.Cancelled) ||
                         productionItem.State.Equals(ProductionState.Done)))
                    {
                        throw new ValidationException(
                        new ValidationResult(
                            new List<ValidationFailure> {
                            new ValidationFailure(nameof(ProductionGroup),
                                                  "Not all of the Production Items have state 'Done' or 'Cancelled'")
                                }
                            )
                        );
                    }
                }
            }

            var histories = _productionService.PrepareProductionGroupHistoreis(productionGroups,
                                                                               request.UserName,
                                                                               ArchiveState.Archived);

            try
            {
                await _archiveProductionGroupRepository.ArchiveRangeProductionGroup(histories);
                await _productionGroupRepository.DeleteRangeProductionGroups(productionGroups);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(ProductionGroup),
                                                    string.Join(", ", request.ProductionGroupIds.ToArray()));
            }
            
            return Unit.Value;
        }
    }
}
