﻿using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem
{
    public class ArchiveItemCommandValidator : AbstractValidator<ArchiveItemCommand>
    {
        public ArchiveItemCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductNumber).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
