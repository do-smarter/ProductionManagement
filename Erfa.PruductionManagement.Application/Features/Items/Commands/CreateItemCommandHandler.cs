using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Features.Items.Queries;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IAsyncRepository<Item> itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = _mapper.Map<Item>(request);
            try
            {
                await _itemRepository.AddAsync(item);
            }catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(Item), request);
            }
            return item.Id;
           
        }
    }
}
