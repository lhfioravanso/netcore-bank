using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}