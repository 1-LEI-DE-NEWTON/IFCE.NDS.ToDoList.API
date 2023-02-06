using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace NDS_ToDo.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
