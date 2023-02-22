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

namespace TestServerApp.Controllers;


public class TestArticleController
{

    [Fact]
    public async Task Get_ShouldReturn200StatusAsync()
    {
        var service = new Mock<IArticleService>();
        MockArticles mockArticles = new();
        service.Setup(_ => _.GetAsync(null)).ReturnsAsync(mockArticles.GetArticles());
        var sut = new ArticleController(service.Object, MockMapper.GetMapper());

        var result = (OkObjectResult)await sut.GetAsync(null);

        result.StatusCode.Should().Be(200);
    }
    [Theory]
    [InlineData(1, true)]
    [InlineData(-4, false)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    public async Task GetById_HasValueAsync(long id, bool hasValue)
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
    public async Task Add_ShouldReturn200StatusAsync()
    {
        var articleService = new Mock<IArticleService>();
        MockArticles mockArticles = new();
        articleService.Setup(_ => _.AddAsync(It.IsAny<Article>())).ReturnsAsync(true);
        var sut = new ArticleController(articleService.Object, MockMapper.GetMapper());

        var result = (OkObjectResult)await sut.AddAsync(new());

        result.StatusCode.Should().Be(200);
    } 
    [Theory]
    [InlineData(1, true)]
    [InlineData(-4, false)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    public async Task Put_TestAsync(long id, bool hasValue)
    {
        var articleService = new Mock<IArticleService>();
        MockArticles mockArticles = new();
        articleService.Setup(_ => _.UpdateAsync(id, It.IsAny<EditArticeDto>())).ReturnsAsync(mockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
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
    public async Task AddComment_HasValueAsync(long id, bool hasValue)
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
