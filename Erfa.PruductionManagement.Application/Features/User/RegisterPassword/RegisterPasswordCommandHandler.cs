using MediatR;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterPassword
{
    public class RegisterPasswordCommandHandler : IRequestHandler<RegisterPasswordCommand>
    {
        public Task<Unit> Handle(RegisterPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
