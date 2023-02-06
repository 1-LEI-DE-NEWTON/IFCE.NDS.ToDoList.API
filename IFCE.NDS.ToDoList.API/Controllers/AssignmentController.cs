using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.Paged;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace IFCE.NDS.ToDoList.API.Controllers
{
    [Authorize]
    [SwaggerTag("Tasks")]

    public class AssignmentsController : MainController
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentsController(INotificator notificator, IAssignmentService assignmentService) : base(notificator)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet]
        [SwaggerOperation("Search tasks")]
        [ProducesResponseType(typeof(PagedDto<AssignmentDto>), StatusCodes.Status200OK)]
        public async Task<PagedDto<AssignmentDto>> Search([FromQuery] AssignmentSearchDto search)
        {
            return await _assignmentService.Search(search);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a tasks")]
        [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var assignment = await _assignmentService.GetById(id);
            return CustomResponse(assignment);
        }

        [HttpPost]
        [SwaggerOperation("Add a new task")]
        [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] AddAssignmentDto model)
        {
            var assignment = await _assignmentService.Add(model);
            return CustomResponse(assignment);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation("Edit a task")]
        [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add(Guid id, [FromBody] EditAssignmentDto model)
        {
            var assignment = await _assignmentService.Edit(id, model);
            return CustomResponse(assignment);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation("Delete a task")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _assignmentService.Delete(id);
            return CustomResponse();
        }

        [HttpPatch("{id:guid}/conclude")]
        [SwaggerOperation("Conclud a task")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Conclude(Guid id)
        {
            await _assignmentService.MarkConcluded(id);
            return CustomResponse();
        }

        [HttpPatch("{id:guid}/unconclude")]
        [SwaggerOperation("Desconclud a task")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Unconclude(Guid id)
        {
            await _assignmentService.MarkNotConcluded(id);
            return CustomResponse();
        }

    }
}
