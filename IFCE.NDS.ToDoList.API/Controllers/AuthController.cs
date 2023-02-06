using Microsoft.AspNetCore.Mvc;
using NDS_ToDo.Application.DTOs.Auth;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace IFCE.NDS.ToDoList.API.Controllers
{
    [SwaggerTag("Auth")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [SwaggerOperation("Register account")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto user)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var token = await _authService.Register(user);
            return CustomResponse(token);
        }

        [HttpPost("login")]
        [SwaggerOperation("Login")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var token = await _authService.Login(user);
            return CustomResponse(token);
        }
    }
}
