using Erfa.PruductionManagement.Application.Features.ProductionItems.Commands.CreateProductionItem;
using Erfa.PruductionManagement.Application.Features.ProductionItems.Queries;
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

        [HttpPost("CreateProductionItem", Name = "Create Production Item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Guid>> CreateNewProductionItem([FromBody] CreateProductionItemCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
