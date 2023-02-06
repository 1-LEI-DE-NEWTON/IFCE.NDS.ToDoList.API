using FluentValidation.Results;

namespace NDS_ToDo.Application.Notifications;

public class Notificator : INotificator
{
    private bool _notFoundResource;
    private readonly List<Notifications> _notifications;
    private INotificator _notificatorImplementation;

    public Notificator()
    {
        _notifications = new List<Notifications>();
    }
    
    public void Handle(Notifications notification)
    {
        if (_notFoundResource)
            throw new InvalidOperationException("Cannot call Handle when there are NotFoundResource notifications");
        _notifications.Add(notification);
    }
    
    public void Handle(List<ValidationFailure> failures)
    {
        failures
            .ForEach(err => Handle(new Notifications(err.ErrorMessage)));
    }

    public void HandleNotFoundResource()
    {
        if (HasNotification)
            throw new InvalidOperationException("Cannot call HandleNotFoundResource when there are notifications");
        _notFoundResource = true;

    }

    public bool HasNotification => _notifications.Any();
    public bool IsNotFoundResource => _notFoundResource;
    public IEnumerable<Notifications> GetNotifications() => _notifications;
    public bool HasNotifications { get; }
}