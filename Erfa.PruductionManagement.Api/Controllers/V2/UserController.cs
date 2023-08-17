using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList;
using Erfa.PruductionManagement.Application.Features.Items;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("demo", Name = "Demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ItemVm>>> Demo()
        {
            var result = await _mediator.Send(new GetItemsListQuery());
            return Ok(result);
        }
    }
}
