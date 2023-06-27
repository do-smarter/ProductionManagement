using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ArchiveProductionItem
{
    public class ArchiveProductionItemCommandValidator : AbstractValidator<ArchiveProductionItemCommand>
    {
        public ArchiveProductionItemCommandValidator()
        {

            RuleFor(p => p.Id).NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
