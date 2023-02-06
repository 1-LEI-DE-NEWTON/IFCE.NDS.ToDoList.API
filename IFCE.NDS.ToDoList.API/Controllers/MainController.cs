using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NDS_ToDo.Application.Notifications;
using FluentValidation.Results;

namespace IFCE.NDS.ToDoList.API.Controllers
{
    public sealed class BadRequestResponse
    {
        public List<string> Erros { get; set; } = new();
    }

    [ApiController]
    [Route("[controller]")]

    public abstract class MainController : Controller
    {
        private readonly INotificator _notificator;

        protected MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida)
            {
                return Ok(result);
            }

            if (_notificator.IsNotFoundResource)
            {
                return NotFound();
            }

            return BadRequest(new BadRequestResponse
            {
                Erros = _notificator.GetNotifications().Select(c => c.Message).ToList()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }


        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        private bool OperacaoValida => !(_notificator.HasNotifications || _notificator.IsNotFoundResource);
        private void AdicionarErroProcessamento(string erro) => _notificator.Handle(new Notifications(erro));
    }



}
