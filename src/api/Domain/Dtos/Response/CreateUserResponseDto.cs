using System;
using Domain.Dtos.Response.Base;

namespace Domain.Dtos.Response
{
    public class CreateUserResponseDto: BaseResponseDto
    {
        public DateTime CreatedAt { get; set; }
        public int AccountId { get; set; }
    }
}