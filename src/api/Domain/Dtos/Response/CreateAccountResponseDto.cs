using System;
using Domain.Dtos.Response.Base;

namespace Domain.Dtos.Response
{
    public class CreateAccountResponseDto: BaseResponseDto
    {
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; }
    }
}