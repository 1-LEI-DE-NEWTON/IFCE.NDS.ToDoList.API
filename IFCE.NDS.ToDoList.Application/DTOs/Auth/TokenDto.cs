using NDS_ToDo.Application.DTOs.User;

namespace NDS_ToDo.Application.DTOs.Auth;

public class TokenDto
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserDto User { get; set; }

}