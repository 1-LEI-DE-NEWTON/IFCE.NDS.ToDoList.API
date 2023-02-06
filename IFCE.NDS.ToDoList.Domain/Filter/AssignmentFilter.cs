namespace NDS_ToDo.Domain.FIlter;

public class AssignmentFilter
{
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDeadline { get; set; }
    public DateTime? EndDeadline { get; set; }
    public bool? Concluded { get; set; }
    public string OrderBy { get; set; } = "description";
    public string OrderDir { get; set; } = "asc";
}