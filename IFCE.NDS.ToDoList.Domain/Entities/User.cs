using System.Collections.ObjectModel;

namespace NDS_ToDo.Domain.Entities;

public class User : Base
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public virtual Collection<Assignment> Assignments { get; set; }
    public virtual Collection<AssignmentList> AssignmentsLists { get; set; } = new();
}