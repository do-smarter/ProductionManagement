using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Contracts.Persistance
{
    public interface IArchiveProductionGroupRepository
    {
        Task<List<ProductionGroupHistory>> ArchiveRangeProductionGroup(List<ProductionGroupHistory> productionGroupHistories);
    }
}
