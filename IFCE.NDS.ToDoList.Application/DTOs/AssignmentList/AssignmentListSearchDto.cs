using NDS_ToDo.Application.DTOs.Paged;

namespace NDS_ToDo.Application.DTOs.AssignmentList;

public class AssignmentListSearchDto : BaseSearchDto
{
    public string name { get; set; }
    public string description { get; set; }
    public string OrderBy { get; set; } = "description";
    public string OrderDir { get; set; } = "asc";
}