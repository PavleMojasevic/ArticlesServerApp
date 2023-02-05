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
    public async Task<IActionResult> GetAsync()
    {
        List<User> articles = await _UserService.GetAsync();
        return Ok(_Mapper.Map<List<UserDTO>>(articles));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        User? user = await _UserService.GetByIdAsync(id);
        if(user==null)
            return NoContent();
        return Ok(_Mapper.Map<UserDTO>(user));
    }
     
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] UserDTO userDTO)
    {
        try
        {
            User user = _Mapper.Map<User>(userDTO);
            return Ok(await _UserService.AddAsync(user));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCredencials credencials)
    {
        try
        {
            JWToken? token = await _UserService.LoginAsync(credencials);
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
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] UserDTO userDTO)
    {
        User user = _Mapper.Map<User>(userDTO);

        if (await _UserService.UpdateAsync(id, user))
            return Ok();
        return BadRequest();
    }
      
}
