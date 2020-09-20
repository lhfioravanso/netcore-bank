using Domain.Dtos.Response.Base;

namespace Domain.Dtos.Response
{
    public class LoginResponseDto: BaseResponseDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
    }
}