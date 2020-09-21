using System;
using Domain.Dtos.Response.Base;
using System.Collections.Generic;

namespace Domain.Dtos.Response
{
    public class UserResponseDto: BaseResponseDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<int> ListAccountId { get; set; }
    }
}