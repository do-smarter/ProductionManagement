using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommandValidator : AbstractValidator<MargeProductionGroupsCommand>
    {
        public MargeProductionGroupsCommandValidator()
        {
            RuleFor(p => p.Groups.Count).GreaterThan(2).WithMessage("There must be at least two elements to be merged");
        }
    }
}
