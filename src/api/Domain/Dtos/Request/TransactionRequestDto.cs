using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Request
{
    public class TransactionRequestDto
    {
        [Required]
        public decimal Value { get; set; }
    }
}