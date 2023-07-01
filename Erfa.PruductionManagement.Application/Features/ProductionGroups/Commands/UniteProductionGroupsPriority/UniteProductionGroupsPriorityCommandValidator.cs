using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.UniteProductionGroupsPriority
{
    public class UniteProductionGroupsPriorityCommandValidator : AbstractValidator<UniteProductionGroupsPriorityCommand>
    {
        public UniteProductionGroupsPriorityCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductionGroupIds).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductionGroupIds.Count).GreaterThan(1)
                .WithMessage("There must be at least two elements to be merged");
            RuleFor(p => p.Priority).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("{PropertyName} is required.");
        }
    }
}
