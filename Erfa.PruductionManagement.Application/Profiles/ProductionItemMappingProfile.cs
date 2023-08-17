using AutoMapper;
using Erfa.PruductionManagement.Application.Features.ProductionItems;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductionItemMappingProfile : Profile
    {
        public ProductionItemMappingProfile()
        {
            CreateMap<ProductionItemModel, ProductionItem>();

            CreateMap<ProductionItem, ProductionItemHistory>()
                .ForMember(pih => pih.Id, e => e.MapFrom(i => Guid.NewGuid()))
                .ForMember(pih => pih.ProductNumber, pi => pi.MapFrom(e => e.Item.ProductNumber))
                .ForMember(pih => pih.ProductionItemId, pi => pi.MapFrom(e => e.Id))
                .ForMember(pih => pih.State, pi => pi.MapFrom(e => e.State.ToString()));

            CreateMap<ProductionItem, ProductionItemVm>()
                .ForMember(vm => vm.Id, pi => pi.MapFrom(e => e.Id))
                .ForMember(vm => vm.ProductNumber, pi => pi.MapFrom(e => e.Item.ProductNumber))
                .ForMember(vm => vm.Description, pi => pi.MapFrom(e => e.Item.Description))
                .ForMember(vm => vm.ProductionTimeSec, pi => pi.MapFrom(e => e.Item.ProductionTimeSec))
                .ForMember(vm => vm.State, pi => pi.MapFrom(e => e.State.ToString()));
        }
    }
}
