using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using ServerApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    ICommentService _CommentService;
    IMapper _Mapper;
    IClaimService _ClaimService;

    public CommentController(ICommentService commentService, IMapper mapper, IClaimService claimService)
    {
        _CommentService = commentService;
        _Mapper = mapper;
        _ClaimService = claimService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _CommentService.GetAsync(_ClaimService.GetUserId(User)));
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddCommentAsync([FromBody] CommentAddDTO commentDTO)
    {

        Comment comment = new(commentDTO.Text, commentDTO.ArticleId);
        comment.AuthorId = _ClaimService.GetUserId(User);
        if (await _CommentService.AddAsync(comment))
            return Ok();
        return BadRequest();
    }

    [HttpPut("Like/{commentId}")]
    public async Task<IActionResult> AddLikeAsync(long commentId)
    {
        long userId = _ClaimService.GetUserId(User);
        if (await _CommentService.AddLikeAsync(commentId, userId))
            return Ok();
        return BadRequest();
    }
    [HttpDelete("Like/{commentId}")]
    public async Task<IActionResult> RemoveLikeAsync(long commentId)
    {
        long userId = _ClaimService.GetUserId(User);
        if (await _CommentService.RemoveLikeAsync(commentId, userId))
            return Ok();
        return BadRequest();
    }
    [HttpPut("Dislike/{commentId}")]
    public async Task<IActionResult> AddDislikeAsync(long commentId)
    {
        long userId = _ClaimService.GetUserId(User);
        if (await _CommentService.AddDislikeAsync(commentId, userId))
            return Ok();
        return BadRequest();
    }
    [HttpDelete("Dislike/{commentId}")]
    public async Task<IActionResult> RemoveDislikeAsync(long commentId)
    {
        long userId = _ClaimService.GetUserId(User);
        if (await _CommentService.RemoveDislikeAsync(commentId, userId))
            return Ok();
        return BadRequest();
    }
    [HttpPost("Approve/{commentId}")]
    public async Task<IActionResult> ApproveAsync(long commentId)
    {
        if (await _CommentService.ApproveAsync(commentId))
            return Ok();
        return BadRequest();
    }
    [HttpPost("Reject/{commentId}")]
    public async Task<IActionResult> RejectAsync(long commentId)
    {
        if (await _CommentService.RejectAsync(commentId))
            return Ok();
        return BadRequest();
    }
    [HttpGet("All")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _CommentService.GetAllAsync());
    }
}
