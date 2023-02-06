using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.Paged;

namespace NDS_ToDo.Application.Services.Contracts;
public interface IAssignmentService
{
    Task<PagedDto<AssignmentDto>> Search(AssignmentSearchDto search);
    Task<AssignmentDto> GetById(Guid id);
    Task<AssignmentDto> Add(AddAssignmentDto assignmentDto);
    Task<AssignmentDto> Edit(Guid id, EditAssignmentDto assignmentDto);
    Task MarkConcluded (Guid id);
    Task MarkNotConcluded (Guid id);
    Task Delete(Guid id);

}