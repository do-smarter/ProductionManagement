using AutoMapper;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Queries;
using Erfa.PruductionManagement.Domain.Entities;

namespace Erfa.PruductionManagement.Application.Profiles
{
    public class ProductionItemMappingProfile : Profile
    {
        public ProductionItemMappingProfile()
        {
            CreateMap<ProductionItem, ProductionItemVm>().ReverseMap();
            CreateMap<CreateProductionItemCommand, ProductionItem>();
        }
    }
}
