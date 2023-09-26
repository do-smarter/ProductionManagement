using Erfa.PruductionManagement.Api.RequestModels;
using Erfa.PruductionManagement.Application.Features.Items;
using Erfa.PruductionManagement.Application.Features.Items.Commands.ArchiveItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.CreateItem;
using Erfa.PruductionManagement.Application.Features.Items.Commands.EditItem;
using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemDetails;
using Erfa.PruductionManagement.Application.Features.Items.Queries.GetItemList;
using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllItems",  Name = "V2 - GetAllItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ItemVm>>> GetAllItems()
        {
            var result = await _mediator.Send(new GetItemsListQuery());
            return Ok(result);
        }

        [HttpGet("GetItemDetails",  Name = "V2 - GetItemByProductNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ItemVm>> GetItemDetails([FromQuery] string ProductNumber)
        {
            var result = await _mediator.Send(new GetItemDetailsQuery(ProductNumber));
            return Ok(result);
        }

        [HttpPost("CreateItem",  Name = "V2 - CreateNewItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CreateNewItem([FromBody] CreateItemRequestModel request,
                                                              [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new CreateItemCommand(request, userName));
            return StatusCode(201, result);
        }

        [HttpPost("CreateItemRange",  Name = "V2 - CreateRangeOfNewItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> CreateRangeOfItems([FromBody] List<CreateItemRequestModel> request,
                                                                         [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new CreateRangeItemsCommand(request, userName));
            return StatusCode(201, result);
        }

        [HttpPut("EditItem",  Name = "V2 - EditItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditItem([FromBody] EditItemRequestModel request,
                                                 [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new EditItemCommand(request, userName));
            return Ok(result);
        }

        [HttpPut("ArchiveItem",  Name = "V2 - ArchiveItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ArchiveItem([FromBody] ArchiveItemRequestModel request,
                                                    [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new ArchiveItemCommand(request, userName));
            return Ok(result);
        }
    }
}
