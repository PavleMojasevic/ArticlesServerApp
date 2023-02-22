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
    IMapper _Mapper;
    public ArticleController(IArticleService articleservice, IMapper mapper)
    {
        _Mapper = mapper;
        _ArticleService = articleservice;
    }
    // GET: api/<ArticleController>
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] ArticleFilterDTO? filter)
    {
        List<Article> articles = await _ArticleService.GetAsync(filter);
        return Ok(_Mapper.Map<List<ArticleDTO>>(articles));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        Article? article = await _ArticleService.GetByIdAsync(id);
        if (article == null)
            return NoContent();
        return Ok(_Mapper.Map<ArticleDTO>(article));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ArticleDTO articleDTO)
    {
        Article article = _Mapper.Map<Article>(articleDTO);
        return Ok(await _ArticleService.AddAsync(article));

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

}
