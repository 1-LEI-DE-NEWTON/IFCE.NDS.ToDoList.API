using NDS_ToDo.Application.DTOs.Paged;

namespace NDS_ToDo.Application.DTOs.Assignment;

public class AssignmentSearchDto : BaseSearchDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }
    public DateTime? StartDeadline { get; set; }
    public DateTime? EndDeadline { get; set; }
    public bool? Concluded { get; set; }
    public string OrderBy { get; set; } = "description";
    public string OrderDir { get; set; } = "asc";

}