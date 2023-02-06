using System.Collections.Generic;

namespace NDS_ToDo.Domain.Entities;

public abstract class Base
{
    protected  Base()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; set; }
    public  DateTime CreatedAt { get; set; }
    public  DateTime UpdatedAt { get; set; }
}
