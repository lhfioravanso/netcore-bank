
using Xunit;
using System;
using Moq;
using Moq.AutoMock;
using Domain.Interfaces.Repositories;
using Domain.Services;
using Domain.Dtos.Request;
using Domain.Dtos.Response;
using Domain.Models;
using Domain.Exceptions;
using System.Collections.Generic;

namespace Test.Domain.Services
{
    public class UserServiceTest
    {

        private Mock<IUserRepository> _mockUserRepository;

        [Fact]
        public void ShouldCreateUser() {
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
        public void ShouldNotCreateUserWithInvalidDto() {     
            _mockUserRepository = new Mock<IUserRepository>();

            UserService userService = new UserService(this._mockUserRepository.Object);

            CreateUserRequestDto dto = new CreateUserRequestDto {
                Name="huashu"
            };

            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(dto));
        }   

        [Fact]
        public void ShouldNotLoginWithInvalidPassword() {
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            User userTest = new User { Username = "testee", PasswordHash = userService.HashPassword("test@pass") };

            mocker.GetMock<IUserRepository>().Setup(c => c.GetUserByUsername("testee"))
                .Returns(userTest);

            LoginRequestDto loginDto = new LoginRequestDto { Username = "testee", Password = "test@pass123" }; 

            Assert.Throws<AuthenticationException>(() => userService.Login(loginDto));
        }

        [Fact]
        public void ShouldLogin() {
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            User userTest = new User { Username = "testee", PasswordHash = userService.HashPassword("test@pass") };

            mocker.GetMock<IUserRepository>().Setup(c => c.GetUserByUsername("testee"))
                .Returns(userTest);

            LoginRequestDto loginDto = new LoginRequestDto { Username = "testee", Password = "test@pass" }; 

            var result = userService.Login(loginDto);

            Assert.Equal(result.Username, userTest.Username);
        }

        [Fact]
        public void ShouldGetUserById() {
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            User userTest = new User { Id = 1 };

            mocker.GetMock<IUserRepository>().Setup(c => c.GetById(1))
                .Returns(userTest);

            var result = userService.GetUserById(1);

            Assert.Equal(result.Id, userTest.Id);
        }


        [Fact]
        public void ShouldThrowNotFoundException() {
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            User userTest = new User { Id = 1 };

            mocker.GetMock<IUserRepository>().Setup(c => c.GetById(1))
                .Returns(userTest);

            Assert.Throws<NotFoundException>(() => userService.GetUserById(2));
        }

        [Fact]
        public void ShouldGetAllUsers () {
            var mocker = new AutoMocker();
            var userService = mocker.CreateInstance<UserService>();

            User user1 = new User { Id = 1 };
            User user2 = new User { Id = 2 };

            List<User> listUser = new List<User>();
            listUser.Add(user1);
            listUser.Add(user2);

            mocker.GetMock<IUserRepository>().Setup(c => c.GetAll())
                .Returns(listUser);

            var result = userService.GetAll();

            Assert.Equal(result[0].Id, 1);
            Assert.Equal(result[1].Id, 2);
        }
    }
}