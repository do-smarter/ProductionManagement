using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterPassword
{
    public class RegisterPasswordCommandValidator : AbstractValidator<RegisterPasswordCommand>
    {
        public RegisterPasswordCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Password).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p.RepeatedPassword).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
            RuleFor(p => p).Must(p => AreEqualPasswords(p))
                .WithMessage("Passworda ar not identical");
            RuleFor(p => p.RegCode).NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }

        private bool AreEqualPasswords(RegisterPasswordCommand command)
        {
            return (command.Password.Equals(command.RepeatedPassword));
        }
    }
}
