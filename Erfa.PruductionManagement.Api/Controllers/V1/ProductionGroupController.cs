using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Queries.GetProductionGroupsList;

namespace Erfa.PruductionManagement.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductionGroupController : Controller
    {
        private readonly IMediator _mediator;

        public ProductionGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProductionGroups", Name = "Get All Production Groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductionGroupVm>>> GetAllGroups()
        {
            var result = await _mediator.Send(new GetProductionGroupsListQuery());
            return Ok(result);
        }

        [HttpPut("MergeGroups", Name = "Merge Production Items Into New Gropus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ProductionGroupVm>>> MergeGroups([FromBody] MargeProductionGroupsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
