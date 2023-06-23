using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(p => p.ProductWeight).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductWeight).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductionTimeSec).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Category).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
