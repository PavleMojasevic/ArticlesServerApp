using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using ServerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServerApp.MockData;

namespace TestServerApp.Controllers
{
    public class TestCategoryController
    {
        Mock<ICategoryService> _CategoryService;  
        CategoryController _CategoryController;

        public TestCategoryController()
        {
            _CategoryService = new Mock<ICategoryService>();
            _CategoryController = new CategoryController(_CategoryService.Object, MockMapper.GetMapper()); 
        }

        [Fact]
        public async Task Add_ShouldReturn400StatusAsync()
        {
            ArgumentException argumentException = new();
            _CategoryService.Setup(_ => _.AddAsync(It.IsAny<Category>())).ReturnsAsync(false);

            var result = (BadRequestResult)await _CategoryController.AddCategoryAsync(new());

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Add_ShouldReturn200StatusAsync()
        {
            ArgumentException argumentException = new();
            _CategoryService.Setup(_ => _.AddAsync(It.IsAny<Category>())).ReturnsAsync(true);

            var result = (OkResult)await _CategoryController.AddCategoryAsync(new());

            result.StatusCode.Should().Be(200);
        } 
        [Fact]
        public async Task Edit_ShouldReturn400StatusAsync()
        {
            ArgumentException argumentException = new();
            _CategoryService.Setup(_ => _.EditAsync(1,It.IsAny<CategoryDTO>())).ReturnsAsync(false);

            var result = (BadRequestResult)await _CategoryController.EditCategoryAsync(1,new());

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Edit_ShouldReturn200StatusAsync()
        {
            ArgumentException argumentException = new();
            _CategoryService.Setup(_ => _.EditAsync(1,It.IsAny<CategoryDTO>())).ReturnsAsync(true);

            var result = (OkResult)await _CategoryController.EditCategoryAsync(1, new());

            result.StatusCode.Should().Be(200);
        } 
        [Fact]
        public async Task Get_ShouldReturn200StatusAsync()
        {
            ArgumentException argumentException = new();
            _CategoryService.Setup(_ => _.GetAsync()).ReturnsAsync(new List<Category>());

            var result = (OkObjectResult)await _CategoryController.GetAsync();

            result.StatusCode.Should().Be(200);
        } 
    }
}
