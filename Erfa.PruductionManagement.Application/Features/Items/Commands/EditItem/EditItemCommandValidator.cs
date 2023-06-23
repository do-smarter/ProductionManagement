using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem
{
    public class EditItemCommandValidator : AbstractValidator<EditItemCommand>
    {
        public EditItemCommandValidator()
        {
            RuleFor(p => p.ProductNumber).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductionTimeSec).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductWeight).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Category).NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
