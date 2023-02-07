using System.ComponentModel.DataAnnotations;

namespace NDS_ToDo.Application.DTOs.Assignment;

public class EditAssignmentDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }
    public DateTime? Deadline { get; set; }
    public Guid? AssignmentListId { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public Guid UserId { get; set; }

}