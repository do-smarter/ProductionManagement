using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem
{
    public class CreateProductionItemCommandValidator : AbstractValidator<CreateProductionItemCommand>
    {
        private readonly ProductionService _productionService;

        public CreateProductionItemCommandValidator(ProductionService productionService)
        {
            _productionService = productionService;

            RuleFor(p => p.ProductNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.OrderNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).IsEnumName(typeof(ProductionState), false)
                                 .WithMessage(_productionService.ProductionStatesListValidationMsg());

        }


    }
}
