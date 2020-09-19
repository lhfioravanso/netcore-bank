using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Interfaces.Repositories;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Domain.Services
{
    public class UserService: BaseService<User>, IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository UserRepository)
            : base(UserRepository)
        {
            _userRepository = UserRepository;
        }

        public virtual CreateUserResponseDto CreateUser(CreateUserRequestDto dto) {
            User user = new User {
                Username = dto.Username,
                Password = dto.Password,
                Name = dto.Name,
                CreatedAt = DateTime.Now
            };

            this._userRepository.Add(user);

            return new CreateUserResponseDto { 
                Id = user.Id,
                CreatedAt = user.CreatedAt,
            };
        }

        public virtual UserResponseDto GetUserById(int id) {
            User user = findUserIfExists(id);

            return new UserResponseDto {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };
        }

        private User findUserIfExists(int id) {
            User user = this._userRepository.GetById(id);

            if (user == null)
                throw new Exception("User not found.");

            return user;
        }
    }
}