using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Erfa.PruductionManagement.Application.Features.User.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegisterNewUserCommandHandler> _logger;
        private readonly ProductionService _productionService;
        private readonly IdentityService _identityService;


        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager,
                                             RoleManager<IdentityRole> roleManager,
                                             IConfiguration configuration,
                                             ILogger<RegisterNewUserCommandHandler> logger,
                                             ProductionService productionService,
                                             IdentityService identityService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _productionService = productionService;
            _identityService = identityService;
        }

        public async Task<string> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            await _productionService.ValidateRequest(request, new RegisterNewUserCommendValidator());

            foreach (var role in request.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    throw new IdentityException($"Role {role} not allowed");
                }
            }

            var userExist = await _userManager.FindByNameAsync(request.UserName);
            if (userExist != null)
            {
                throw new IdentityException($"Username {request.UserName} not awaialable");
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };


            IdentityResult saved = await _userManager.CreateAsync(user);

            if (saved.Succeeded)
            {
                foreach (var role in request.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                string regCode = _identityService.GenerateRegCode();
                string hashedRegCode = _identityService.HashString(regCode);
                user.RegCodeHash = hashedRegCode;
                IdentityResult updated = await _userManager.UpdateAsync(user);
                if (!updated.Succeeded)
                {
                    throw new PersistanceFailedException(nameof(ApplicationUser), request.UserName);
                }
                return regCode;
            }
            else throw new PersistanceFailedException(nameof(ApplicationUser), request.UserName);
        }
    }
}
