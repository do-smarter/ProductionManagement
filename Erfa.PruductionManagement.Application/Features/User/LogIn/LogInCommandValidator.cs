using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.User.LogIn
{
    public class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Password).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
