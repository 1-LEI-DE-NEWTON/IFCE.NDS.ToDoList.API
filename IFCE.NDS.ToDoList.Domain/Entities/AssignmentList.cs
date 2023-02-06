using System.Collections.ObjectModel;

namespace NDS_ToDo.Domain.Entities;

public class AssignmentList : Base
{
    public string Name { get; set; }
    public Guid UserId { get; set; }

    // EF Relation
    public virtual User User { get; set; }
    public virtual Collection<Assignment> Assignments { get; set; } = new();

}