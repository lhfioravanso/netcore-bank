using System;

namespace Domain.Dtos.Response
{
    public class AccountResponseDto
    {
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; }
    }
}