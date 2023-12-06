using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Archive;
using Erfa.PruductionManagement.Domain.Entities.Production;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, string>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;


        public CreateItemCommandHandler(IAsyncRepository<Item> itemRepository,
                                        IMapper mapper,
                                        ILogger<CreateItemCommandHandler> logger, ProductionService productionService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<string> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Item item = _mapper.Map<Item>(request);
            item.CreatedBy = request.UserName;
            item.LastModifiedBy = request.UserName;

            try
            {
                //TODO send event
                await _itemRepository.AddAsync(item);
            }
            catch (Exception ex)
            {
                throw new PersistanceFailedException(nameof(Item), request);
            }
            return item.ProductNumber;

        }
    }
}
