using Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateRangeItems;
using Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem;
using Erfa.PruductionManagement.Application.Features.Items.Queries;
using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemDetails;
using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList;
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

        [HttpGet("GetAllItems", Name = "GetAllItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ItemVm>>> GetAllItems()
        {
            var result = await _mediator.Send(new GetItemsListQuery());
            return Ok(result);
        }

        [HttpGet("GetItemDetails", Name = "GetItemByProductNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemVm>> GetItemDetails([FromQuery] string ProductNumber)
        {
            var result = await _mediator.Send(new GetItemDetailsQuery(ProductNumber));
            return Ok(result);
        }

        [HttpPost("CreateItem", Name = "CreateNewItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateNewItem([FromBody] CreateItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("CreateItemRange", Name = "CreateRangeOfNewItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> CreateRangeOfItems([FromBody] CreateRangeItemsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditItem", Name = "EditItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditItem([FromBody] EditItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("ArchiveItem", Name = "ArchiveItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ArchiveItem([FromBody] ArchiveItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
