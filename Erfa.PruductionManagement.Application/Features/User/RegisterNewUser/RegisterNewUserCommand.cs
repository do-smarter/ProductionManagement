using MediatR;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterNewUser
{
    public class RegisterNewUserCommand : IRequest<string>
    {
        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string[]? Roles { get; set; }
    }
}
