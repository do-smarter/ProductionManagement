using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem
{
    public class EditProductionItemCommandValidator : AbstractValidator<EditProductionItemCommand>
    {
        public EditProductionItemCommandValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Comment).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.OrderNumber).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Quantity).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.RalGalv).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}