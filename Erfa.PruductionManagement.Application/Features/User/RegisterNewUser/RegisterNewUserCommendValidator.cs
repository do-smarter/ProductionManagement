using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterNewUser
{
    public class RegisterNewUserCommendValidator : AbstractValidator<RegisterNewUserCommand>
    {
        public RegisterNewUserCommendValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty().WithMessage("UserName is required");
            RuleFor(p => p.FirstName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.LastName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Roles).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
