using AutoMapper;
using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductionGroupMappingProfile : Profile
    {
        public ProductionGroupMappingProfile()
        {
            CreateMap<ProductionGroup, ProductionGroupVm>();
            CreateMap<ProductionGroup, ProductionGroupHistory>()
                .ForMember(pgh => pgh.ProductionGroupId, pg => pg.MapFrom(pg => pg.Id))
                .ForMember(pgh => pgh.ProductionItems, pg => pg.MapFrom(pg => pg.ProductionItems))
                ;
                 
        }
    }
}
