using AutoMapper;
using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductionGroupMappingProfile : Profile
    {
        public ProductionGroupMappingProfile()
        {
            CreateMap<ProductionGroup, ProductionGroupVm>();
            CreateMap<ProductionGroup, ProductionGroupHistory>()
                .ForMember(pgh => pgh.Id, e => e.MapFrom(i => Guid.NewGuid()))
                .ForMember(pgh => pgh.ProductionGroupId, pg => pg.MapFrom(pg => pg.Id))
                .ForMember(pgh => pgh.ProductionItems, pg => pg.MapFrom(pg => pg.ProductionItems));
        }
    }
}
