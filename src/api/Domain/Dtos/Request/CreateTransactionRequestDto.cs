using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class CreateTransactionRequestDto
    {
        [Required]
        public decimal Value { get; set; }
    }
}