    using System.ComponentModel.DataAnnotations;

namespace NDS_ToDo.Application.DTOs.Assignment;

public class AddAssignmentDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }
    public DateTime? Deadline { get; set; }
    public Guid? AssignmentListId { get; set; } = null;

}