using NDS_ToDo.Application.DTOs.Auth;

namespace NDS_ToDo.Application.Services.Contracts;
public interface IAuthService
{
    Task<TokenDto> Login(LoginDto user);
    Task<TokenDto> Register(RegisterDto user);

}