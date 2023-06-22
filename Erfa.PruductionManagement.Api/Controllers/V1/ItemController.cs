using Erfa.PruductionManagement.Application.Features.Items.Commands;
using Erfa.PruductionManagement.Application.Features.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("AllItems", Name = "GetAllItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ItemVm>>> GetAllItems()
        {
            var itemVmList = await _mediator.Send(new GetItemsListQuery());
            return Ok(itemVmList);
        }

        [HttpGet("ItemDetails", Name = "GetItemByProductNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemVm>> GetItemDetails([FromQuery] string ProductNumber)
        {
            var itemVm = await _mediator.Send(new GetItemDetailsQuery(ProductNumber));
            return Ok(itemVm);
        }

        [HttpGet("CreateItem", Name = "CreateNewItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemVm>> CreateNewItem([FromBody] CreateItemCommand command)
        {
            var itemVm = await _mediator.Send(command);
            return Ok(itemVm);
        }
    }
}
