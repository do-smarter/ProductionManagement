using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState
{
    public class ChangeProductionItemStateCommandValidator : AbstractValidator<ChangeProductionItemStateCommand>
    {
        public ChangeProductionItemStateCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty()
                               .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).IsEnumName(typeof(ProductionState), false)
                                .WithMessage(ProductionService.ProductionStatesListValidationMsg());
        }
    }
}
