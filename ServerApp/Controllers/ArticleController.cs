using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;
 

namespace ServerApp.Controllers
{
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
        public async Task<IActionResult> Get()
        {
            List<Article> articles = await _ArticleService.Get(); 
            return Ok(_Mapper.Map<List<ArticleDTO>>(articles));
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Article article = await _ArticleService.GetById(id);
            return Ok(_Mapper.Map<ArticleDTO>(article)); 
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                Article article = _Mapper.Map<Article>(articleDTO);
                return Ok(await _ArticleService.Add(article)); 
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ArticleDTO articleDTO)
        {
            Article article = _Mapper.Map<Article>(articleDTO); 

            if (await _ArticleService.Update(id, article))
                return Ok();
            return BadRequest();
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ArticleService.DeleteById(id))
                return Ok();
            return BadRequest(); 
        }
        [HttpPost("Comment")]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentAddDTO commentDTO)
        {
            Comment comment = _Mapper.Map<Comment>(commentDTO);
            //TODO

            if (await _ArticleService.AddComment(comment))
                return Ok();
            return BadRequest(); 
        }
        
    }
}
