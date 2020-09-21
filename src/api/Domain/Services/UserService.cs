using System;
using Domain.Services.Base;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Interfaces.Repositories;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Domain.Messages;
using System.Linq;

namespace Domain.Services
{
    public class UserService: BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public virtual CreateUserResponseDto CreateUser(CreateUserRequestDto dto) {
            
            this.ValidateUserCreation(dto.Username);
            
            User user = new User {
                Username = dto.Username,
                Name = dto.Name,
                CreatedAt = DateTime.Now
            };

            user.PasswordHash = this.HashPassword(dto.Password);
            this._userRepository.Add(user);

            return new CreateUserResponseDto { 
                Id = user.Id,
                CreatedAt = user.CreatedAt,
            };
        }

        public virtual LoginResponseDto Login(LoginRequestDto dto) { 
            User user = this.FindUserByUsername(dto.Username);

            if (user == null)
                throw new AuthenticationException(MessagesUtil.InvalidCredentials);
     
            if (!this.ValidateHashPassword(dto.Password, user.PasswordHash))
                throw new AuthenticationException(MessagesUtil.InvalidCredentials);

            return new LoginResponseDto { 
                Id = user.Id, 
                Username=user.Username, 
                Name = user.Name
            };
        }

        public virtual UserResponseDto GetUserById(int id) {
            User user = FindUserIfExists(id);

            UserResponseDto response = new UserResponseDto {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };

            if (user.Accounts != null && user.Accounts.Count > 0) {
                response.ListAccountId = user.Accounts.Select(acc => acc.Id).ToList();
            }

            return response;
        }

        private void ValidateUserCreation(string username){
            User user = this.FindUserByUsername(username);

            if (user != null)
                throw new BusinessException(MessagesUtil.UsernameAlreadyUsed);
        }
        private User FindUserIfExists(int id) {
            User user = this._userRepository.GetById(id);

            if (user == null)
                throw new NotFoundException(MessagesUtil.UserNotFound);

            return user;
        }
        private User FindUserByUsername(string username) {
            User user = this._userRepository.GetUserByUsername(username);
            return user;
        }
        private bool ValidateHashPassword(string password, string passwordHash) {
            try
            {
                PasswordVerificationResult passwordVerificationResult = new PasswordHasher<object>().VerifyHashedPassword(null, passwordHash, password);
                switch (passwordVerificationResult)
                {
                    case PasswordVerificationResult.Success:
                    case PasswordVerificationResult.SuccessRehashNeeded:
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private string HashPassword(string password) {
            return new PasswordHasher<object>().HashPassword(null, password);
        }
    }
}