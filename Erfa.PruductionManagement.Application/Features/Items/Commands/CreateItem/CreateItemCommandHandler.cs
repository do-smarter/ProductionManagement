using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, string>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;
        private ILogger<CreateItemCommandHandler> _logger { get; }


        public CreateItemCommandHandler(IAsyncRepository<Item> itemRepository, IMapper mapper, ILogger<CreateItemCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemCommandValidator();
            await ProductionService.ValidateRequest(request, validator);

            Item item = _mapper.Map<Item>(request);
            item.CreatedBy = request.UserName;
            try
            {
                await _itemRepository.AddAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new PersistanceFailedException(nameof(Item), request);
            }
            return item.ProductNumber;

        }
    }
}
