using AutoMapper;
using Erfa.PruductionManagement.Api.RequestModels;
using Erfa.PruductionManagement.Application.Features.Items;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<CreateItemRequestModel, Item>();
            CreateMap<EditItemCommand, Item>().
                ForMember(i => i.LastModifiedBy, e => e.MapFrom(eic => eic.UserName));
            CreateMap<Item, ItemVm>().ReverseMap();
            CreateMap<Item, ItemHistory>();
        }
    }
}
