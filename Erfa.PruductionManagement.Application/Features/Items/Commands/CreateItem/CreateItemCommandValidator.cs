﻿using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty().WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.MaterialProductName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductionTimeSec).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
