using Microsoft.AspNetCore.Authorization;
using NDS_ToDo.Application.Notifications;
using Swashbuckle.AspNetCore.Annotations;

namespace IFCE.NDS.ToDoList.API.Controllers
{
    [Authorize]
    [SwaggerTag("Account")]

    public class AccountController : MainController
    {
        public AccountController(INotificator notificator) : base(notificator)
        {
        }
    }
}
