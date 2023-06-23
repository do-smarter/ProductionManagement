using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem
{
    public class ArchiveItemCommandValidator : AbstractValidator<ArchiveItemCommand>
    {
        public ArchiveItemCommandValidator()
        {
            RuleFor(p => p.ProductNumber).NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
