using Erfa.PruductionManagement.Application.RequestModels;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.CreateProductionGroup
{
    public class ProductionItemModelValidator : AbstractValidator<ProductionItemModel>
    {
        public ProductionItemModelValidator()
        {
            RuleFor(p => p.ProductNumber).NotNull().NotEmpty()
                    .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.OrderNumber).NotNull().NotEmpty()
                    .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Quantity).NotNull().NotEmpty().GreaterThan(0)
                    .WithMessage("{PropertyName} is required.");
        }
    }
}
