using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities.Production;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList
{
    internal class GetItemsListQueryHandler : IRequestHandler<GetItemsListQuery, List<ItemVm>>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public GetItemsListQueryHandler(IMapper mapper, IAsyncRepository<Item> itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemVm>> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            var allItems = (await _itemRepository.ListAllAsync()).OrderBy(x => x.ProductNumber).ToList();
            return _mapper.Map<List<ItemVm>>(allItems);
        }
    }
}
