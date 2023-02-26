using AutoMapper;
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

namespace TestServerApp.Controllers;


public class TestArticleController
{

    Mock<IArticleService> _ArticleService;
    Mock<ICommentService> _CommentService ;
    MockArticles _MockArticles ;

    public TestArticleController()
    {
        _ArticleService = new Mock<IArticleService>();
        _CommentService = new Mock<ICommentService>();
        _MockArticles = new();
    }

    [Fact]
    public async Task Get_ShouldReturn200StatusAsync()
    {
        MockArticles mockArticles = new();
        _ArticleService.Setup(_ => _.GetAsync(null)).ReturnsAsync(mockArticles.GetArticles());
        var sut = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper());

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
        _ArticleService.Setup(_ => _.GetByIdAsync(id)).ReturnsAsync(_MockArticles.GetArticles().FirstOrDefault(x => x.Id == id));
        var sut = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper());

        var result = await sut.GetByIdAsync(id);
        if (hasValue)
            Assert.IsType<OkObjectResult>(result);
        else
            Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task Add_ShouldReturn200StatusAsync()
    {  
        _ArticleService.Setup(_ => _.AddAsync(It.IsAny<Article>()));
        var sut = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper());

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
        _ArticleService.Setup(_ => _.UpdateAsync(id, It.IsAny<EditArticeDto>()))
            .ReturnsAsync(_MockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
        var sut = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper());

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
        _ArticleService.Setup(_ => _.AddCommentAsync(It.IsAny<Comment>())).ReturnsAsync(_MockArticles.GetArticles().FirstOrDefault(x => x.Id == id) != null);
        var sut = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper());

        var result = await sut.AddCommentAsync(new());
        if (hasValue)
            Assert.IsType<OkResult>(result);
        else
            Assert.IsType<BadRequestResult>(result);
    }
}
