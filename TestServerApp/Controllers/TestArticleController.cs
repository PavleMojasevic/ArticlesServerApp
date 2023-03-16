using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using System.Security.Claims;
using TestServerApp.MockData;

namespace TestServerApp.Controllers;


public class TestArticleController
{

    Mock<IArticleService> _ArticleService;
    Mock<ICommentService> _CommentService;
    Mock<IClaimService> _ClaimsService;
    MockArticles _MockArticles;
    ArticleController _ArticleController;

    public TestArticleController()
    {
        _ArticleService = new Mock<IArticleService>();
        _CommentService = new Mock<ICommentService>();
        _ClaimsService = new Mock<IClaimService>();
        _MockArticles = new();
        _ClaimsService.Setup(_ => _.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(1);
        _ArticleController = new ArticleController(_ArticleService.Object, _CommentService.Object, MockMapper.GetMapper(), _ClaimsService.Object);


    }

    [Fact]
    public async Task Get_ShouldReturn204StatusAsync()
    {
        MockArticles mockArticles = new();
        Article? article = null;
        _ArticleService.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(article);

        var result = (NoContentResult)await _ArticleController.GetByIdAsync(1);

        result.StatusCode.Should().Be(204);
    }
    [Fact]
    public async Task Get_ShouldReturn200StatusAsync()
    {
        MockArticles mockArticles = new();
        Article? article = new();
        _ArticleService.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(article);

        var result = (OkObjectResult)await _ArticleController.GetByIdAsync(1);

        result.StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task GetById_ShouldReturn200StatusAsync()
    {
        MockArticles mockArticles = new();
        _ArticleService.Setup(_ => _.GetAsync(null)).ReturnsAsync(mockArticles.GetArticles());

        var result = (OkObjectResult)await _ArticleController.GetAsync(null);

        result.StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task GetById_ShouldReturn204StatusAsync()
    {
        MockArticles mockArticles = new();
        Article? article = null;
        _ArticleService.Setup(_ => _.GetByIdAsync(1)).ReturnsAsync(article);
        _CommentService.Setup(_ => _.GetByArticleAsync(1, 1)).ReturnsAsync(new List<CommentDTO>());

        var result = (NoContentResult)await _ArticleController.GetByIdAsync(1);

        result.StatusCode.Should().Be(204);
    }
    [Fact]
    public async Task Add_ShouldReturn400StatusAsync()
    {
        ArgumentException argumentException = new();
        _ArticleService.Setup(_ => _.AddAsync(It.IsAny<Article>())).Throws(argumentException);

        var result = (BadRequestObjectResult)await _ArticleController.AddAsync(new());

        result.StatusCode.Should().Be(400);
    }
    [Fact]
    public async Task Add_ShouldReturn200StatusAsync()
    {
        ArgumentException argumentException = new();
        _ArticleService.Setup(_ => _.AddAsync(It.IsAny<Article>()));

        var result = (OkResult)await _ArticleController.AddAsync(new());

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

        var result = await _ArticleController.UpdateAsync(id, new());
        if (hasValue)
            Assert.IsType<OkResult>(result);
        else
            Assert.IsType<BadRequestResult>(result);
    }
}
