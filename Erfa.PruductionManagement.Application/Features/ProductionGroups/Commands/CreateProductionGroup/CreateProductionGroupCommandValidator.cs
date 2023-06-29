using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.CreateProductionGroup
{
    internal class CreateProductionGroupCommandValidator : AbstractValidator<CreateProductionGroupCommand>
    {
        public CreateProductionGroupCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductionItems).NotNull().NotEmpty()
                .WithMessage("At leats one ProductionItem must be provided.");
            RuleForEach(p => p.ProductionItems).SetValidator(new ProductionItemModelValidator());
        }
    }
}