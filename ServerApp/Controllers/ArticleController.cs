using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
using System.Security.Claims;

namespace ServerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    IArticleService _ArticleService;
    ICommentService _CommentService;
    IMapper _Mapper;
    IClaimService _ClaimService;

    public ArticleController(IArticleService articleService, ICommentService commentService, IMapper mapper, IClaimService claimService)
    {
        _ArticleService = articleService;
        _CommentService = commentService;
        _Mapper = mapper;
        _ClaimService = claimService;
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

        result.Comments = _Mapper.Map<List<CommentDTO>>(await _CommentService.GetByArticleAsync(articleId, _ClaimService.GetUserId(User)));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ArticleAddDTO articleDTO)
    { 
        Article article = _Mapper.Map<Article>(articleDTO);
        article.AuthorId = _ClaimService.GetUserId(User);
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
    [HttpPut("{articleId}")]
    public async Task<IActionResult> UpdateAsync(long articleId, [FromBody] EditArticeDto editArticeDto)
    {
        if (await _ArticleService.UpdateAsync(articleId, editArticeDto))
            return Ok();
        return BadRequest();
    }  

}
