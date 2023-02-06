namespace NDS_ToDo.Application.Notifications;

public class Notifications
{
    public string Message { get; set; }
    
    public Notifications(string message)
    {
        Message = message;
    }
}