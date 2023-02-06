using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NDS_ToDo.Application.DTOs.Auth;

public class RegisterDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string PasswordConfirm { get; set; }

}