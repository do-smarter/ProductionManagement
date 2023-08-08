using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.TakeDownProductionGroup
{
    public class TakeDownProductionGroupCommandValidator : AbstractValidator<TakeDownProductionGroupCommand>
    {
        public TakeDownProductionGroupCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductionGroupIds).NotNull().NotEmpty()
                .WithMessage("No Production Groups proviced");
            RuleForEach(p => p.ProductionGroupIds).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
