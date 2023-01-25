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
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        IUserService _UserService;
        IMapper _Mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _Mapper = mapper;
            _UserService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<User> articles = await _UserService.Get();
            return Ok(_Mapper.Map<List<UserDTO>>(articles));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(long id)
        {
            User? user = await _UserService.GetById(id);
            if(user==null)
                return BadRequest();
            return Ok(_Mapper.Map<UserDTO>(user));
        }
         
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
        {
            try
            {
                User user = _Mapper.Map<User>(userDTO);
                return Ok(await _UserService.Add(user));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredencials credencials)
        {
            try
            {
                JWToken? token = await _UserService.Login(credencials);
                if (token == null)
                    return BadRequest();
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UserDTO userDTO)
        {
            User user = _Mapper.Map<User>(userDTO);

            if (await _UserService.Update(id, user))
                return Ok();
            return BadRequest();
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (await _UserService.DeleteById(id))
                return Ok();
            return BadRequest();
        }
    }
}
