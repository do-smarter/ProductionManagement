using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands
{
    public class CreateRangeItemsCommandValidator : AbstractValidator<CreateRangeItemsCommand>
    {
        public CreateRangeItemsCommandValidator()
        {
            RuleFor(e => e).NotEmpty().WithMessage("No items provided");
        }
    }
}
