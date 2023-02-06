using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.AssignmentList;
using NDS_ToDo.Application.DTOs.Paged;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace IFCE.NDS.ToDoList.API.Controllers
{
    [Authorize]
    [SwaggerTag("To-do lists")]
    public class AssignmentListController : MainController
    {
        private readonly IAssignmentListService _assignmentListService;

        public AssignmentListController(INotificator notificator, IAssignmentListService assignmentListService) : base(notificator)
        {
            _assignmentListService = assignmentListService;
        }

        [HttpGet]
        [SwaggerOperation("Search to-do lists")]
        [ProducesResponseType(typeof(PagedDto<AssignmentListDto>), StatusCodes.Status200OK)]
        public async Task<PagedDto<AssignmentListDto>> Search([FromQuery] AssignmentListSearchDto search)
        {
            return await _assignmentListService.Search(search);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a to-do list")]
        [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var list = await _assignmentListService.GetById(id);
            return CustomResponse(list);
        }

        [HttpGet("{id}/assignments")]
        [SwaggerOperation("Search for tasks in a to-do list")]
        [ProducesResponseType(typeof(IEnumerable<AssignmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAssignment(Guid id, [FromQuery] AssignmentSearchDto search)
        {
            var list = await _assignmentListService.SearchAssignments(id, search);
            return CustomResponse(list);
        }

        [HttpPost]
        [SwaggerOperation("Add a new to-do list")]
        [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] AddAssignmentListDto model)
        {
            var list = await _assignmentListService.Add(model);
            return CustomResponse(list);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation("Edit a to-do list")]
        [ProducesResponseType(typeof(AssignmentListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add(Guid id, [FromBody] EditAssignmentListDto model)
        {
            var list = await _assignmentListService.Edit(id, model);
            return CustomResponse(list);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation("Delete a to-do list")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _assignmentListService.Delete(id);
            return CustomResponse();
        }


    }
}
