using Erfa.PruductionManagement.Application.RequestModels;
using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems
{
    public class CreateRangeItemsCommandValidator : AbstractValidator<CreateRangeItemsCommand>
    {
        public CreateRangeItemsCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("Headers are missing {PropertyName}.");
            RuleFor(e => e.Items).NotEmpty()
                .WithMessage("No items provided");
        }
    }
}
