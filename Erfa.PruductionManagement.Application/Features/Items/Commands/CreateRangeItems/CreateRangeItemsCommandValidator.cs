using Erfa.PruductionManagement.Api.RequestModels;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
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
            RuleForEach(e => e.Items).SetValidator(new CreateItemRequestModelValidator());
        }
    }

}

public class CreateItemRequestModelValidator : AbstractValidator<CreateItemRequestModel>
{
    public CreateItemRequestModelValidator()
    {
        RuleFor(p => p.ProductNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        /*
        RuleFor(p => p.MaterialProductName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.ProductionTimeSec).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(p => p.Description).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        */
    }
}