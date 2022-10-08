using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        [TestCase]
        public async Task Get_Should_Return_OK_Result()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var returnValue = new List<User>() { new User() { EmailId = "abcd@g.com", Id = 3, MonthlyExpense = 90, MonthlySalary = 500, Name = "abcd"} };
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.GetAllUsers()).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [TestCase]
        public async Task Get_Should_Return_NotFound_Result()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var returnValue = new List<User>();
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.GetAllUsers()).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TestCase(0)]
        public async Task GetById_Should_Return_BadRequest_Result(int id)
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var returnValue = new User();
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.GetUserById(id)).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [TestCase(1000)]
        public async Task GetById_Should_Return_NotFound_Result(int id)
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            User user = null;
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.GetUserById(id)).ReturnsAsync(user);
            //Act
            var result = await controller.Get(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [TestCase(1)]
        public async Task GetById_Should_Return_OK_Result(int id)
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var returnValue = new User() { EmailId = "abcd@g.com", Id = 3, MonthlyExpense = 90, MonthlySalary = 500, Name = "abcd" };
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.GetUserById(id)).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_BadRequest_Result_When_User_Is_Null()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            User user = null;
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.CheckIfUserExists("")).ReturnsAsync(true);
            //Act
            var result = await controller.Post(user);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_BadRequest_Result_When_User_Already_Exists()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var user = new User() { EmailId = "abcd@g.com", Id = 3, MonthlyExpense = 90, MonthlySalary = 500, Name = "abcd" };
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.CheckIfUserExists(user.EmailId)).ReturnsAsync(true);
            //Act
            var result = await controller.Post(user);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_OK_Result_When_User_Does_Not_Exists()
        {
            //Arrange
            var userService = new Mock<IUserService>();
            var userServiceObj = userService.Object;
            var user = new User() { EmailId = "abcd@g.com", MonthlyExpense = 90, MonthlySalary = 500, Name = "abcd" };
            var controller = new UsersController(userServiceObj);
            //SetUp
            userService.Setup(s => s.CheckIfUserExists(user.EmailId)).ReturnsAsync(false);
            //Act
            var result = await controller.Post(user);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
