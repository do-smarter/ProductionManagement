using MediatR;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterPassword
{
    public class RegisterPasswordCommand : IRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RepeatedPassword { get; set; }
        public string? RegCode { get; set; }
    }
}
