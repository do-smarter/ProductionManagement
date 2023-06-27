using AutoMapper;
using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductionGroupMappingProfile : Profile
    {
        public ProductionGroupMappingProfile()
        {
            CreateMap<ProductionGroup, ProductionGroupVm>().
                 ForMember(vm => vm.GroupState, pg => pg.MapFrom(e => e.GroupState.ToString()));
        }
    }
}
