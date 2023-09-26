using Erfa.PruductionManagement.Application.Features.ProductionItems;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Queries;
using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ProductionItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductionItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProductionItems",  Name = "V2 - Get All Production Items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductionItemVm>>> GetAllItems()
        {
            var result = await _mediator.Send(new GetProductionItemsListQuery());
            return Ok(result);
        }

        [HttpPut("EditProductionItem",  Name = "V2 - Edit Production Item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditProductionItem([FromBody] EditProductionItemRequestModel request,
                                                           [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new EditProductionItemCommand(request, userName));
            return Ok(result);
        }
        [HttpPut("ChangeProductionItemState",  Name = "V2 - Change Production Item's State")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> State([FromBody] ChangeProductionItemStateRequestModel request,
                                              [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new ChangeProductionItemStateCommand(request, userName));
            return Ok(result);
        }
    }
}
