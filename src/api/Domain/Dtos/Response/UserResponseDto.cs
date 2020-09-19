using System;
using Domain.Dtos.Response.Base;

namespace Domain.Dtos.Response
{
    public class UserResponseDto: BaseResponseDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}