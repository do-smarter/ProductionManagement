using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState
{
    public class ChangeProductionItemStateCommandValidator : AbstractValidator<ChangeProductionItemStateCommand>
    {
        private readonly ProductionService _productionService;
        public ChangeProductionItemStateCommandValidator(ProductionService productionService)
        {
            _productionService = productionService;

            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductionItemId).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).IsEnumName(typeof(ProductionState), false)
                .WithMessage(_productionService.ProductionStatesListValidationMsg());
        }
    }
}
