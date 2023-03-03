using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using System;
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
}
