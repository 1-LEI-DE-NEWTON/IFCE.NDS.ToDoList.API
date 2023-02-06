using NDS_ToDo.Application.DTOs.AssignmentList;

namespace NDS_ToDo.Application.DTOs.Assignment;

public class AssignmentDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Guid? AssignmentListId { get; set; }
    public DateTime? Deadline { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public AssignmentListDto AssignmentList { get; set; }

}