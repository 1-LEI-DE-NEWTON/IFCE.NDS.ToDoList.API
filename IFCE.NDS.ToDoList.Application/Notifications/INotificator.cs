using FluentValidation.Results;

namespace NDS_ToDo.Application.Notifications;

public interface INotificator
{
    void Handle(Notifications notification);
    void Handle(List<ValidationFailure> failures);
    void HandleNotFoundResource();
    IEnumerable<Notifications> GetNotifications();
    bool HasNotifications {get;}
    bool IsNotFoundResource {get;}
}