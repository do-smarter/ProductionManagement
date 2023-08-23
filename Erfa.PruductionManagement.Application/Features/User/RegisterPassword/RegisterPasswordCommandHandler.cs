using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterPassword
{
    public class RegisterPasswordCommandHandler : IRequestHandler<RegisterPasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterPasswordCommandHandler> _logger;
        private readonly ProductionService _productionService;
        private readonly IdentityService _identityService;

        public RegisterPasswordCommandHandler(UserManager<ApplicationUser> userManager,
                                              ILogger<RegisterPasswordCommandHandler> logger,
                                              ProductionService productionService,
                                              IdentityService identityService)
        {
            _userManager = userManager;
            _logger = logger;
            _productionService = productionService;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(RegisterPasswordCommand request, CancellationToken cancellationToken)
        {
            await _productionService.ValidateRequest(request, new RegisterPasswordCommandValidator());

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new IdentityException("Provided combitation of username and registration code does not exist");
            }

            var regCodeHash = user.RegCodeHash;
            var hashedProvidedRegCode = _identityService.HashString(request.RegCode);

            if (!regCodeHash.Equals(hashedProvidedRegCode))
            {
                throw new IdentityException("Provided combitation of username and registration code does not exist");
            }

            IdentityResult userPass = await _userManager.AddPasswordAsync(user, request.Password);
            if (!userPass.Succeeded)
            {
                throw new IdentityException("PasswordRequiresNonAlphanumeric, PasswordRequiresDigit, PasswordRequiresUpper");
            }
            else
            {
                user.Active = true;
                user.SignedUp = true;
                user.RegCodeHash = null;

                IdentityResult userUpd = await _userManager.UpdateAsync(user);
                if (!userUpd.Succeeded)
                {
                    throw new IdentityException("Password Registration failed");
                }
            }
            return Unit.Value;
        }
    }
}
