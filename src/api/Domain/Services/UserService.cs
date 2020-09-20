using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Interfaces.Repositories;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Exceptions;

namespace Domain.Services
{
    public class UserService: BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository UserRepository)
            : base(UserRepository)
        {
            _userRepository = UserRepository;
        }

        public virtual CreateUserResponseDto CreateUser(CreateUserRequestDto dto) {

            this.ValidateUserCreation(dto.Username);
            
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
            User user = FindUserIfExists(id);

            return new UserResponseDto {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };
        }

        private void ValidateUserCreation(string username){
            User user = this.FindUserByUsername(username);

            if (user != null)
                throw new BusinessException("Username is already in use.");
        }

        private User FindUserIfExists(int id) {
            User user = this._userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException("User not found.");

            return user;
        }

        private User FindUserByUsername(string username) {
            User user = this._userRepository.GetUserByUsername(username);
            return user;
        }
    }
}