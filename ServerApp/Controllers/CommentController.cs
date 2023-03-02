using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using ServerApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        ICommentService _CommentService;
        IMapper _Mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _CommentService = commentService;
            _Mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            string? userIdStr = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (userIdStr == null)
            {
                return Unauthorized();
            }
            long userId = Convert.ToInt64(userIdStr);
            List<CommentDTO> comments = _Mapper.Map<List<CommentDTO>>(await _CommentService.GetAsync(userId));
            return Ok(comments);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCommentAsync([FromBody] CommentAddDTO commentDTO)
        {

            Comment comment = new(commentDTO.Text, commentDTO.ArticleId);
            comment.AuthorId = GetUserId()??0;
            if (await _CommentService.AddAsync(comment))
                return Ok();
            return BadRequest();
        }

        [HttpPut("Like/{commentId}")]
        public async Task<IActionResult> AddLikeAsync(long commentId)
        {
            long? userId = GetUserId();
            if (userId != null)
            {
                if (await _CommentService.AddLikeAsync(commentId, (long)userId))
                    return Ok();
                return BadRequest();
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpDelete("Like/{commentId}")]
        public async Task<IActionResult> RemoveLikeAsync(long commentId)
        {
            long? userId = GetUserId();
            if (userId != null)
            {
                if (await _CommentService.RemoveLikeAsync(commentId, (long)userId))
                    return Ok();
                return BadRequest();
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPut("Dislike/{commentId}")]
        public async Task<IActionResult> AddDislikeAsync(long commentId)
        {
            long? userId = GetUserId();
            if (userId != null)
            {
                if (await _CommentService.AddDislikeAsync(commentId, (long)userId))
                    return Ok();
                return BadRequest();
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpDelete("Dislike/{commentId}")]
        public async Task<IActionResult> RemoveDislikeAsync(long commentId)
        {
            long? userId = GetUserId() ;
            if (userId != null)
            {
                if (await _CommentService.RemoveDislikeAsync(commentId, (long)userId))
                    return Ok();
                return BadRequest();
            }
            else
            {
                return Unauthorized();
            } 
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
        private long? GetUserId()
        {
            string? userIdStr = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (userIdStr != null)
            {
                return Convert.ToInt64(userIdStr); 
            }
            return null;
        }
    }
}
