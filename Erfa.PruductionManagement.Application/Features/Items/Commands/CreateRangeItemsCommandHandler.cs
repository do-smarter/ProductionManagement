using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    public class CreateRangeItemsCommandHandler : IRequestHandler<CreateRangeItemsCommand, List<string>>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateItemCommandHandler> _logger;

        public CreateRangeItemsCommandHandler(IAsyncRepository<Item> itemRepository, IMapper mapper, ILogger<CreateItemCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public Task<List<string>> Handle(CreateRangeItemsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

