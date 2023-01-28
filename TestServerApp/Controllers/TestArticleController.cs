using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServerApp.MockData;

namespace TestServerApp.Controllers
{

    public class TestArticleController
    {

        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.Get()).ReturnsAsync(mockArticles.GetArticles());
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.Get();

            result.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task GetById_HasValue(long id, bool hasValue)
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.GetById(id)).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id));
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = await sut.GetById(id);
            if (hasValue)
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task Add_ShouldReturn200Status()
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.Add(It.IsAny<Article>())).ReturnsAsync(true);
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.Add(new());

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Add_ShouldReturn400Status()
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            Article article = new Article();
            todoService.Setup(_ => _.Add(It.IsAny<Article>())).Throws(new Exception());
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = (BadRequestObjectResult)await sut.Add(new());

            result.StatusCode.Should().Be(400);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task Put_Test(long id, bool hasValue)
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.Update(id, It.IsAny<Article>())).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = await sut.Put(id, new());
            if (hasValue)
                Assert.IsType<OkResult>(result);
            else
                Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task Delete_HasValue(long id, bool hasValue)
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.DeleteById(id)).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = await sut.Delete(id);
            if (hasValue)
                Assert.IsType<OkResult>(result);
            else
                Assert.IsType<BadRequestResult>(result);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        [InlineData(-4, false)]
        public async Task AddComment_HasValue(long id, bool hasValue)
        {
            var todoService = new Mock<IArticleService>();
            MockArticles mockArticles = new MockArticles();
            todoService.Setup(_ => _.AddComment(It.IsAny<Comment>())).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
            var sut = new ArticleController(todoService.Object, MockMapper.GetMapper());

            var result = await sut.AddComment(new());
            if (hasValue)
                Assert.IsType<OkResult>(result);
            else
                Assert.IsType<BadRequestResult>(result);
        }
    }
}
