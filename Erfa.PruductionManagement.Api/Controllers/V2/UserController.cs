using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList;
using Erfa.PruductionManagement.Application.Features.Items;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Erfa.PruductionManagement.Application.Features.User.RegisterNewUser;

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
        public async Task<ActionResult<string>> Demo(RegisterNewUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
