using MediatR;

namespace Erfa.PruductionManagement.Application.Features.User.LogIn
{
    public class LogInCommand : IRequest<(string,LoginResponseVm)>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
