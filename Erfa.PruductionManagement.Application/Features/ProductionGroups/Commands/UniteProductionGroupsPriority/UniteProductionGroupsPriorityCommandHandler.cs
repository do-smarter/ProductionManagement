using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
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

            List<ProductionGroup> mergedProductionGroups = await _productionGroupRepository.FindListOfGroupsByIds(uniqeGroups);

            if (mergedProductionGroups.Count != uniqeGroups.Count)
            {
                string ids = String.Join(", or ", uniqeGroups.ToArray());
                throw new ResourceNotFoundException(nameof(ProductionGroup), ids);
            }

            var histories = _productionService.PrepareProductionGroupHistoreis(mergedProductionGroups,
                                                                               request.UserName,
                                                                               ArchiveState.Archived);

            ProductionGroup result = new ProductionGroup();
            mergedProductionGroups.ForEach(g => result.ProductionItems.AddRange(g.ProductionItems));
            result.CreatedBy = request.UserName;
            result.LastModifiedBy = request.UserName;

            try
            {
                await _archiveProductionGroupRepository.ArchiveRangeProductionGroup(histories)
                    .ContinueWith(async r => await _productionGroupRepository.DeleteRangeAsync(mergedProductionGroups))
                    .ContinueWith(async r => await _productionService.AddSingleProductionGroupPriority(result, request.Priority));


                // TODO: AddingNewEventArgs METHOD IN REPOSITORY TO HANDLE ALL THE CHANGES
            }
            catch
            {
                throw new PersistanceFailedException(nameof(ProductionGroup),
                                                    string.Join(", ", request.ProductionGroupIds.ToArray()));
            }


            return _mapper.Map<ProductionGroupVm>(result);
        }
    }
}
