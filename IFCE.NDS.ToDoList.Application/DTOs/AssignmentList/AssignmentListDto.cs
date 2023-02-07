using NDS_ToDo.Application.DTOs.Assignment;

namespace NDS_ToDo.Application.DTOs.AssignmentList;

public class AssignmentListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<AssignmentDto> Assignments { get; set; }

}