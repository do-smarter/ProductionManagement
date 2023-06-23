using AutoMapper;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem;
using Erfa.PruductionManagement.Application.Features.Items.Queries;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Queries;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<CreateItemCommand, Item>();
            CreateMap<EditItemCommand, Item>();
            CreateMap<Item, ItemVm>().ReverseMap();
            CreateMap<Item, ItemHistory>();
        }
    }
}
