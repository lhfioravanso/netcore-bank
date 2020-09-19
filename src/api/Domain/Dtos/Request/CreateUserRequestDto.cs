using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class CreateUserRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
    }
}