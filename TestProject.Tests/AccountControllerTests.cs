using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.DTO;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [TestCase]
        public async Task Get_Should_Return_OK_Result()
        {
            //Arrange
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            var returnValue = new List<AccountDTO>() { new AccountDTO() { Id=1, Type="Savings" , Balance = 5000, UserId = 1} };
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.GetAllAccounts()).ReturnsAsync(returnValue);
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
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            var returnValue = new List<AccountDTO>();
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.GetAllAccounts()).ReturnsAsync(returnValue);
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
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            var returnValue = new AccountDTO();
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.GetAccountById(id)).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get(It.IsAny<int>());
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [TestCase(-1)]
        public async Task GetById_Should_Return_NotFound_Result(int id)
        {
            //Arrange
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            AccountDTO returnValue = null;
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.GetAccountById(id)).ReturnsAsync(returnValue);
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
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            AccountDTO returnValue = new AccountDTO() { Id = 1, Type = "Savings", Balance = 5000, UserId = 1 };
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.GetAccountById(id)).ReturnsAsync(returnValue);
            //Act
            var result = await controller.Get(id);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_BadRequest_Result_When_Account_Is_Null()
        {
            //Arrange
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            AccountDTO account = null;
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.CanUserCreateAccount(account)).ReturnsAsync(false);
            //Act
            var result = await controller.Post(account);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_BadRequest_Result_When_User_Not_Eligible_To_Create_Account()
        {
            //Arrange
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            AccountDTO account = new AccountDTO() { Id = 1, Type = "Savings", Balance = 5000, UserId = 10 }; ;
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.CanUserCreateAccount(account)).ReturnsAsync(false);
            //Act
            var result = await controller.Post(account);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [TestCase()]
        public async Task Post_Should_Return_OK_Result_When_User_Is_Eligible_To_Create_Account()
        {
            //Arrange
            var accountService = new Mock<IAccountService>();
            var accountServiceObj = accountService.Object;
            AccountDTO account = new AccountDTO() { Id = 1, Type = "Savings", Balance = 5000, UserId = 1 }; ;
            var controller = new AccountsController(accountServiceObj);
            //SetUp
            accountService.Setup(s => s.CanUserCreateAccount(account)).ReturnsAsync(true);
            //Act
            var result = await controller.Post(account);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
