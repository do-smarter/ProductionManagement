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
                .WithMessage("{PropertyName} is required.")
                .Must(p => AreUniqueIds(p))
                .WithMessage("Elements must be unique.");
            RuleFor(p => p.ProductionGroupIds.Count).GreaterThan(1)
                .WithMessage("There must be at least two elements to be merged");
            RuleFor(p => p.Priority).NotNull().NotEmpty().GreaterThan(0)
                .WithMessage("{PropertyName} is required.");
        }

        private bool AreUniqueIds(List<Guid> ids)
        {
            HashSet<Guid> uniqeGroups = new HashSet<Guid>(ids);
            return uniqeGroups.Count == ids.Count;
        }
    }
}
