using Erfa.PruductionManagement.Application.Features.ProductionGroups;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Queries.GetProductionGroupsList;
using Erfa.PruductionManagement.Application.RequestModels;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.CreateProductionGroup;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.TakeDownProductionGroup;
using Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.UniteProductionGroupsPriority;

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
        public async Task<ActionResult<List<ProductionGroupVm>>> MergeGroups([FromBody] MergeProductionGroupsRequestModel request,
                                                                             [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new MargeProductionGroupsCommand(request, userName));
            return Ok(result);
        }

        [HttpPost("AddNewProductionGroup", Name = "Add New Production Group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductionGroupVm>> AddNewProductionGroup([FromBody] List<ProductionItemModel> request,
                                                                             [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new CreateProductionGroupCommand(request, userName));
            return Ok(result);
        }

        [HttpDelete("TakeDownProductionGroup", Name = "Take Down Production Group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductionGroupVm>> TakeDownProductionGroup([FromBody] TakeDownProductionGroupsRequestModel request,
                                                                     [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new TakeDownProductionGroupCommand(request, userName));
            return Ok(result);
        }

        [HttpPut("UniteProductionGroupsPriority", Name = "Unite Productio nGroups Priority")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductionGroupVm>> UniteProductionGroupsPriority(
                                                                   [FromBody] UniteProductionGroupsPriorityRequestModel request,
                                                                   [FromHeader] ApiHeaders apiHeaders)
        {
            string userName = apiHeaders.UserName;

            var result = await _mediator.Send(new UniteProductionGroupsPriorityCommand(request, userName));
            return Ok(result);
        }
    }
}
