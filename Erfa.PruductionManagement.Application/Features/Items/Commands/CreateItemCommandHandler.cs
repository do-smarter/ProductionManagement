﻿using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, string>
    {

        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IAsyncRepository<Item> itemRepository, IMapper mapper, ILogger<CreateItemCommandHandler> logger)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        private ILogger<CreateItemCommandHandler> _logger { get; }


        public async Task<string> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemCommandValidator();
            var validationResults = await validator.ValidateAsync(request);
            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }

            Item item = _mapper.Map<Item>(request);
            try
            {
                await _itemRepository.AddAsync(item);
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new PersistanceFailedException(nameof(Item), request);
            }
            return item.ProductNumber;
           
        }
    }
}
