using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using PB_EGS_Escalavel.API.Controllers;
using PB_EGS_Escalavel.Application.InputModels;
using PB_EGS_Escalavel.Application.Services.Interfaces;
using PB_EGS_Escalavel.Application.ViewModels;
using PB_EGS_Escalavel.Core.Auth;
using PB_EGS_Escalavel.Core.Entities;
using PB_EGS_Escalavel.Core.Repositories;
using PB_EGS_Escalavel.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EGS_Escalavel.UnitTeste.Application.Services
{
    public class UserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;

        public UserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _accountServiceMock = new Mock<IAccountService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task InputDataIsOk_Executed_ReturnUserId()
        {
            // Arrange

            var newUserInputModel = new NewUserInputModel
            {
                FullName = "TestInput",
                Password = "1234567890",
                Email = "caio@test.com.br",
                BirthDate = new DateTime(2001, 10, 11),
                Role = "student"
            };

            var user = new User
            (
                "TestUser",
                "caio@test.com.br",
                new DateTime(2001, 10, 11),
                "1234567890",
                "student"
            );


            //Act            
            _userServiceMock.Setup(x => x.CreateAsync(It.IsAny<NewUserInputModel>())).Returns(Task.FromResult(1));

            var controller = new UsersController(_userServiceMock.Object, _accountServiceMock.Object);
            var actual = controller.Post(newUserInputModel);

            //Assert
            Assert.True(actual.Id >= 0);

            _userRepositoryMock.Verify(us => us.AddAsync(user), Times.Never);
        }

        [Fact]
        public async Task GetDataIdIsOk_Executed_ReturnUser()
        {
            // Arrange

            var userViewModel = new UserViewModel
            (
                "TestInput",
                "caio@test.com.br"
            );



            //Act            
            _userServiceMock.Setup(x => x.GetUserAsync(It.IsAny<int>())).Returns(Task.FromResult(userViewModel));

            var controller = new UsersController(_userServiceMock.Object, _accountServiceMock.Object);
            var actual = controller.GetById(1);
            var actualResult = actual.Result as OkObjectResult;

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);

            _userRepositoryMock.Verify(us => us.GetByIdAsync(1), Times.Never);
        }

        [Fact]
        public async Task LoginDataIsOk_Executed_ReturnUser()
        {
            // Arrange

            var loginUserInputModel = new LoginUserInputModel
            {
                Email ="caio@test.com.br",
                Password = "1234567890"
            };

            var loginUserViewModel = new LoginUserViewModel
            (
                "caio@test.com.br",
                "dgsrgrshaaaaaahrtherhberhr516511fhb65dfnd"
            );

            var user = new User
            (
                "TestUser",
                "caio@test.com.br",
                new DateTime(2001, 10, 11),
                "1234567890",
                "student"
            );


            //Act            
            _accountServiceMock.Setup(x => x.Login(It.IsAny<LoginUserInputModel>())).Returns(Task.FromResult(loginUserViewModel));

            var controller = new UsersController(_userServiceMock.Object, _accountServiceMock.Object);
            var actual = controller.Login(loginUserInputModel);
            var actualResult = actual.Result as OkObjectResult;

            //Assert
            Assert.NotNull(actualResult);
            Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);

            _authServiceMock.Verify(at => at.ComputeSha256Hash(loginUserInputModel.Password), Times.Never);
            _userRepositoryMock.Verify(us => us.GetUserByEmailAndPasswordAsync(loginUserInputModel.Email, loginUserInputModel.Password), Times.Never);
            _authServiceMock.Verify(us => us.GenerateJwtToken(user.Email, user.Role), Times.Never);
        }
    }
}
