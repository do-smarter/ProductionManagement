using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Erfa.PruductionManagement.Application.Features.User.LogIn
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, (string, LoginResponseVm)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LogInCommandHandler> _logger;
        private readonly IdentityService _identityService;

        public LogInCommandHandler(UserManager<ApplicationUser> userManager,
                                   ILogger<LogInCommandHandler> logger,
                                   IdentityService identityService)
        {
            _userManager = userManager;
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<(string, LoginResponseVm)> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new IdentityException("Signing in failed");
            }

            if (!user.Active || !user.SignedUp)
            {
                throw new IdentityException("Signing in failed");
            }

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new IdentityException("Signing in failed");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var authClaim = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }
            JwtSecurityToken jwtToken = _identityService.GetToken(authClaim);
            var loginResponseVm = new LoginResponseVm()
            {
                Expires = jwtToken.ValidTo,
                Roles = userRoles.ToArray(),
                Username = user.UserName
            };
            return (tokenHandler.WriteToken(jwtToken), loginResponseVm);
        }


    }
}
