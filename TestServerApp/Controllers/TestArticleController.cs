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
            var service = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            service.Setup(_ => _.GetAsync()).ReturnsAsync(mockArticles.GetArticles());
            var sut = new ArticleController(service.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.GetAsync();

            result.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task GetById_HasValue(long id, bool hasValue)
        {
            var articleService = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            articleService.Setup(_ => _.GetByIdAsync(id)).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id));
            var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

            var result = await sut.GetByIdAsync(id);
            if (hasValue)
                Assert.IsType<OkObjectResult>(result);
            else
                Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task Add_ShouldReturn200Status()
        {
            var articleService = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            articleService.Setup(_ => _.Add(It.IsAny<Article>())).ReturnsAsync(true);
            var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

            var result = (OkObjectResult)await sut.AddAsync(new());

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Add_ShouldReturn400Status()
        {
            var articleService = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            Article article = new Article();
            articleService.Setup(_ => _.Add(It.IsAny<Article>())).Throws(new Exception());
            var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

            var result = (BadRequestObjectResult)await sut.AddAsync(new());

            result.StatusCode.Should().Be(400);
        }
        [Theory]
        [InlineData(1, true)]
        [InlineData(-4, false)]
        [InlineData(2, true)]
        [InlineData(3, false)]
        public async Task Put_Test(long id, bool hasValue)
        {
            var articleService = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            articleService.Setup(_ => _.UpdateAsync(id, It.IsAny<Article>())).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
            var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

            var result = await sut.UpdateAsync(id, new());
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
            var articleService = new Mock<IArticleService>();
            MockArticles mockArticles = new();
            articleService.Setup(_ => _.AddCommentAsync(It.IsAny<Comment>())).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
            var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

            var result = await sut.AddCommentAsync(new());
            if (hasValue)
                Assert.IsType<OkResult>(result);
            else
                Assert.IsType<BadRequestResult>(result);
        }
    }
}
