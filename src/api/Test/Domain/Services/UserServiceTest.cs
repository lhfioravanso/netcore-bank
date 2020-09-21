
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;

namespace Test.Domain.Services
{
    public class UserServiceTest
    {

        private Mock<IUserRepository> _mockUserRepository;

        [Fact]
        public void shouldCreateUser() {

            _mockUserRepository = new Mock<IUserRepository>();

            UserService userService = new UserService(this._mockUserRepository.Object);

            CreateUserRequestDto dto = new CreateUserRequestDto {
                Name="huashu",
                Password = "teste123",
                Username = "teste"
            };

            CreateUserResponseDto result = userService.CreateUser(dto);
            Assert.Equal(result.Id, 0);
        }

        [Fact]
        public void shouldNotCreateUser() {     
            _mockUserRepository = new Mock<IUserRepository>();

            UserService userService = new UserService(this._mockUserRepository.Object);

            CreateUserRequestDto dto = new CreateUserRequestDto {
                Name="huashu"
            };

            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(dto));
        }
    }
}