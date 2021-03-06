using System;
using Domain.Dtos.Response.Base;

namespace Domain.Dtos.Response
{
    public class AccountResponseDto: BaseResponseDto
    {
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; }
    }
}