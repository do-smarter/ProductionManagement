using Erfa.PruductionManagement.Domain.Entities.Archive;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IArchiveProductionGroupRepository
    {
        Task<List<ProductionGroupHistory>> ArchiveRangeProductionGroup(List<ProductionGroupHistory> productionGroupHistories);
    }
}
