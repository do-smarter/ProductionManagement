using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems
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
        public async Task<List<string>> Handle(CreateRangeItemsCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateRangeItemsCommandValidator();
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }

            List<Item> items = _mapper.Map<List<Item>>(request);
            try
            {
                await _itemRepository.AddRangeAsync(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new PersistanceFailedException(nameof(Item), request);
            }

            List<string> result = new List<string>();

            items.ForEach(item => result.Add(item.ProductNumber));

            return result;
        }
    }
}

