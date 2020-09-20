using Domain.Interfaces.Services.Base;
using Domain.Models;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Domain.Interfaces.Services
{
    public interface IUserService: IBaseService<User>
    {
        CreateUserResponseDto CreateUser(CreateUserRequestDto dto);
        UserResponseDto GetUserById(int id);
        LoginResponseDto Login(LoginRequestDto dto);
    }
}