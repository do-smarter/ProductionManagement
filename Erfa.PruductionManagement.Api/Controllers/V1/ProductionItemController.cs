using Erfa.PruductionManagement.Application.Features.ProductionItems;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ArchiveProductionItem;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.ChangeProductionState;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.EditProductionItem;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Queries;
using Erfa.PruductionManagement.Application.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erfa.PruductionManagement.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductionItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductionItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProductionItems", Name = "Get All Production Items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductionItemVm>>> GetAllItems()
        {
            var result = await _mediator.Send(new GetProductionItemsListQuery());
            return Ok(result);
        }

        [HttpPut("EditProductionItem", Name = "Edit Production Item")]
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
        [HttpPut("ChangeProductionItemState", Name = "Change Production Item's State")]
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

        [HttpDelete("ArchiveProductionItem", Name = "Archive Production Item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ArchiveProductionItem([FromBody] ArchiveProductionItemCommand request,
                                                              [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;
            var result = await _mediator.Send(request);
            return Ok(result);
        }



    }
}
