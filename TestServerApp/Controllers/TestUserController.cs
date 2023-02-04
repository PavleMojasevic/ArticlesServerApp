using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Interfaces;
using FluentAssertions;
using ServerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServerApp.MockData;
using ServerApp.Models;
using ServerApp.Infrastucture.Configurations;

namespace TestServerApp.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var userService = new Mock<IUserService>();
            MockUsers mockUsers = new();
            userService.Setup(_ => _.GetAsync()).ReturnsAsync(mockUsers.GetUsers());
            var sut = new UsersController(userService.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.GetAsync();

            result.StatusCode.Should().Be(200);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)] 
        public async Task GetById_HasValue(long id, bool hasValue)
        {
            var userService = new Mock<IUserService>();
            MockUsers mockUsers = new();
            userService.Setup(_ => _.GetByIdAsync(id)).ReturnsAsync(mockUsers.GetUsers().FirstOrDefault(x => x.Id == id));
            var sut = new UsersController(userService.Object, MockMapper.GetMapper());

            var result = await sut.GetByIdAsync(id);
            if (hasValue)
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task Add_ShouldReturn200Status()
        {
            var userService = new Mock<IUserService>();
            MockUsers mockUsers = new();
            userService.Setup(_ => _.Add(It.IsAny<User>())).ReturnsAsync(true);
            var sut = new UsersController(userService.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.AddAsync(new());

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Add_ShouldReturn400Status()
        {
            var userService = new Mock<IUserService>();
            MockUsers mockUsers = new();
            Article article = new();
            userService.Setup(_ => _.Add(It.IsAny<User>())).Throws(new Exception());
            var sut = new UsersController(userService.Object, MockMapper.GetMapper());

            var result = (BadRequestObjectResult)await sut.AddAsync(new());

            result.StatusCode.Should().Be(400);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        public async Task Put_Test(long id, bool hasValue)
        {

            var userService = new Mock<IUserService>();
            MockUsers mockUsers = new();
            userService.Setup(_ => _.UpdateAsync(id, It.IsAny<User>())).ReturnsAsync(mockUsers.GetUsers().FirstOrDefault(x => x.Id == id) != null);

            var sut = new UsersController(userService.Object, MockMapper.GetMapper());

            var result = await sut.UpdateAsync(id, new());
            if (hasValue)
                Assert.IsType<OkResult>(result);
            else
                Assert.IsType<BadRequestResult>(result);
        } 
    }
}
