using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Erfa.PruductionManagement.Application.Features.User.LogIn
{
    public class LogInCommand : IRequest<JwtTokenVm>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
