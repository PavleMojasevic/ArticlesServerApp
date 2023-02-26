using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;


namespace ServerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    IArticleService _ArticleService;
    ICommentService _CommentService;
    IMapper _Mapper;

    public ArticleController(IArticleService articleService, ICommentService commentService, IMapper mapper)
    {
        _ArticleService = articleService;
        _CommentService = commentService;
        _Mapper = mapper;
    }

    // GET: api/<ArticleController>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] ArticleFilterDTO? filter)
    {
        List<Article> articles = await _ArticleService.GetAsync(filter);
        return Ok(_Mapper.Map<List<ArticleDTO>>(articles));
    }

    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetByIdAsync(long articleId)
    {
        Article? article = await _ArticleService.GetByIdAsync(articleId);
        if (article == null)
            return NoContent();
        var result = _Mapper.Map<ArticleDTO>(article);

        long? userId = GetUserId();
        result.Comments = _Mapper.Map<List<CommentDTO>>(await _CommentService.GetByArticleAsync(userId, articleId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ArticleAddDTO articleDTO)
    { 
        Article article = _Mapper.Map<Article>(articleDTO);
        article.AuthorId = GetUserId();
        try
        {
            await _ArticleService.AddAsync(article);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    // PUT api/<ArticleController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] EditArticeDto editArticeDto)
    {
        if (await _ArticleService.UpdateAsync(id, editArticeDto))
            return Ok();
        return BadRequest();
    }

    [HttpPost("Comment")]
    [Authorize]
    public async Task<IActionResult> AddCommentAsync(CommentAddDTO commentDTO)
    {
        Comment comment = _Mapper.Map<Comment>(commentDTO);
        //TODO

        if (await _ArticleService.AddCommentAsync(comment))
            return Ok();
        return BadRequest();
    }
    private long GetUserId()
    {
        string? userIdStr = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        return Convert.ToInt64(userIdStr);
    }

}
