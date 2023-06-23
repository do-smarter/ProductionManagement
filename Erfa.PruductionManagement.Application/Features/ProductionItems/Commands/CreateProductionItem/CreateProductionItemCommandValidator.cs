using Erfa.PruductionManagement.Domain.Enums;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem
{
    public class CreateProductionItemCommandValidator : AbstractValidator<CreateProductionItemCommand>
    {
        public CreateProductionItemCommandValidator()
        {

            RuleFor(p => p.ProductNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.OrderNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.State).IsEnumName(typeof(ProductionState), false)
                .WithMessage(ProductionStatesList());
        }

        private string ProductionStatesList()
        {
            var stateList = Enum.GetValues(typeof(ProductionState)).Cast<ProductionState>().ToList();

            string message = "{PropertyName} must be one of:";
            foreach (var state in stateList)
            {
                message += " " + state.ToString();
                
            }
            return message;
        }
    }
}
