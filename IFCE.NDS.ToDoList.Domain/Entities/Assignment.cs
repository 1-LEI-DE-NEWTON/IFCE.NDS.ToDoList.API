namespace NDS_ToDo.Domain.Entities;

public class Assignment : Base
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public  Guid UserId { get; set; }
    public Guid? AssingmentListId { get; set; }
    public DateTime? Deadline { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; private set; }
    
    public User User { get; set; }
    public AssignmentList AssignmentList { get; set; }
    
    public void SetConclude()
    {
        Concluded = true;
        ConcludedAt = DateTime.Now;
    }
    
    public void SetUnconclude()
    {
        Concluded = false;
        ConcludedAt = null;
    }
}