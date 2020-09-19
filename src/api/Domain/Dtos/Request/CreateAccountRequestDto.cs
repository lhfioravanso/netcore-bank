using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class CreateAccountRequestDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Bank { get; set; }
        [Required]
        public string Agency { get; set; }
        [Required]
        public string Number { get; set; }
    }
}