using AutoMapper;
using Erfa.PruductionManagement.Application.Features.Items.Queries;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<Item, ItemVm>().ReverseMap();
            CreateMap<Item, ItemHistory>();
           
            /*CreateMap<Education, EducationDto>()
                .ForMember(dto => dto.DunCode, ent => ent.MapFrom(e => e.EducationCode.DunCode));
        }*/
    }
}
