using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
