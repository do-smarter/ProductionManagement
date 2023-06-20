using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Items.Queries
{
    public class GetItemDetailsQueryValidator : AbstractValidator<GetItemDetailsQuery>
    {
        public GetItemDetailsQueryValidator()
        {
            RuleFor(p => p.ProductNumber).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }

    }
}
