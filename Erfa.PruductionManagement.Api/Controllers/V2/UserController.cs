using MediatR;
using Microsoft.AspNetCore.Mvc;
using Erfa.PruductionManagement.Application.Features.User.RegisterNewUser;
using Erfa.PruductionManagement.Application.Features.User.RegisterPassword;
using Erfa.PruductionManagement.Application.Features.User.LogIn;
using Erfa.PruductionManagement.Application.Features.User;

namespace Erfa.PruductionManagement.Api.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterUser", Name = "Register New User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> RegisterUser(RegisterNewUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("RegisterPassword", Name = "Register Password For Registered User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> RegisterPassword(RegisterPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("LogIn", Name = "Log In")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseVm>> LogIn(LogInCommand command)
        {
            var result = await _mediator.Send(command);

            Response.Cookies
                .Append("X-Access-Token", result.Item1,
                new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                });

            return Ok(result.Item2);
        }
        //TODO add filters for authorization
    }
}
