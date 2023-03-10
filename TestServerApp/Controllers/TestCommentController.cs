using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Interfaces;
using ServerApp.Models;
using System.Security.Claims;
using TestServerApp.MockData;

namespace TestServerApp.Controllers;


public class TestCommentController
{
     
    Mock<ICommentService> _CommentService ;
    Mock<IClaimService> _ClaimsService ;
    MockArticles _MockArticles;
    CommentController _CommentController;

    public TestCommentController()
    { 
        _CommentService = new Mock<ICommentService>();
        _ClaimsService = new Mock<IClaimService>();
        _MockArticles = new();
        _ClaimsService.Setup(_ => _.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(1);
        _CommentController = new CommentController(_CommentService.Object, MockMapper.GetMapper(), _ClaimsService.Object); 


    }

    [Fact]
    public async Task Get_ShouldReturn200StatusAsync()
    {
        MockComment mockComments = new();
        _CommentService.Setup(_ => _.GetAsync(1)).ReturnsAsync(mockComments.GetCommentsDTO()); 

        var result = (OkObjectResult)await _CommentController.GetAsync();

        result.StatusCode.Should().Be(200);
    } 
    [Fact]
    public async Task GetAll_ShouldReturn200StatusAsync()
    {
        MockComment mockComments = new();
        _CommentService.Setup(_ => _.GetAsync(1)).ReturnsAsync(mockComments.GetCommentsDTO()); 

        var result = (OkObjectResult)await _CommentController.GetAllAsync();

        result.StatusCode.Should().Be(200);
    } 
    [Fact]
    public async Task Add_ShouldReturn400StatusAsync()
    { 
        _CommentService.Setup(_ => _.AddAsync(It.IsAny<Comment>())).ReturnsAsync(false); 

        var result = (BadRequestResult)await _CommentController.AddCommentAsync(new());

        result.StatusCode.Should().Be(400);
    }
    [Fact]
    public async Task Add_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.AddAsync(It.IsAny<Comment>())).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.AddCommentAsync(new());

        result.StatusCode.Should().Be(200);
    }  
    [Fact]
    public async Task LikeComment_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.AddLikeAsync(1,1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.AddLikeAsync(1);

        result.StatusCode.Should().Be(200);
    }  
    [Fact]
    public async Task LikeComment_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.AddLikeAsync(1,1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.AddLikeAsync(1);

        result.StatusCode.Should().Be(400);
    }  
    [Fact]
    public async Task RemoveLike_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.RemoveLikeAsync(1,1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.RemoveLikeAsync(1);

        result.StatusCode.Should().Be(200);
    }  
    [Fact]
    public async Task RemoveLike_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.RemoveLikeAsync(1,1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.RemoveLikeAsync(1);

        result.StatusCode.Should().Be(400);
    }  
    [Fact]
    public async Task DislikeComment_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.AddDislikeAsync(1,1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.AddDislikeAsync(1);

        result.StatusCode.Should().Be(200);
    }  
    [Fact]
    public async Task DislikeComment_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.AddDislikeAsync(1,1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.AddDislikeAsync(1);

        result.StatusCode.Should().Be(400);
    }  
    [Fact]
    public async Task RemoveDislike_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.RemoveDislikeAsync(1,1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.RemoveDislikeAsync(1);

        result.StatusCode.Should().Be(200);
    }  
    [Fact]
    public async Task RemoveDislike_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.RemoveDislikeAsync(1,1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.RemoveDislikeAsync(1);

        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task ApproveComment_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.ApproveAsync(1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.ApproveAsync(1);

        result.StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task ApproveComment_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.ApproveAsync(1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.ApproveAsync(1);

        result.StatusCode.Should().Be(400);
    }
    [Fact]
    public async Task RejectComment_ShouldReturn200StatusAsync()
    {
        _CommentService.Setup(_ => _.RejectAsync(1)).ReturnsAsync(true);

        var result = (OkResult)await _CommentController.RejectAsync(1);

        result.StatusCode.Should().Be(200);
    }
    [Fact]
    public async Task RejectComment_ShouldReturn400StatusAsync()
    {
        _CommentService.Setup(_ => _.RejectAsync(1)).ReturnsAsync(false);

        var result = (BadRequestResult)await _CommentController.RejectAsync(1);

        result.StatusCode.Should().Be(400);
    }
}
