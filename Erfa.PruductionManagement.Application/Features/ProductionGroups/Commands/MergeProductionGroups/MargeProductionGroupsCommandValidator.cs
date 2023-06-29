using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommandValidator : AbstractValidator<MargeProductionGroupsCommand>
    {
        public MargeProductionGroupsCommandValidator()
        {

            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductionGroupIds.Count).GreaterThan(1)
                .WithMessage("There must be at least two elements to be merged");
        }
    }
}
