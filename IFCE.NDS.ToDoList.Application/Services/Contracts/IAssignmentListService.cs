using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.AssignmentList;
using NDS_ToDo.Application.DTOs.Paged;

namespace NDS_ToDo.Application.Services.Contracts;

public interface IAssignmentListService
{
    Task<PagedDto<AssignmentListDto>> Search(AssignmentListSearchDto search);
    Task<PagedDto<AssignmentDto>> SearchAssignments(Guid id, AssignmentSearchDto search);
    Task<AssignmentListDto> GetById(Guid id);
    Task<AssignmentListDto> Add(AddAssignmentListDto assignmentListDto);
    Task<AssignmentListDto> Edit(Guid id, EditAssignmentListDto assignmentListDto);
    Task Delete(Guid id);


}