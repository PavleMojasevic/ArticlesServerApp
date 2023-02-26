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
public class UserController : ControllerBase
{
    // GET: api/<UsersController>
    IUserService _UserService;
    IMapper _Mapper;
    public UserController(IUserService userService, IMapper mapper)
    {
        _Mapper = mapper;
        _UserService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        List<User>? articles = await _UserService.GetAsync();
        return Ok(_Mapper.Map<List<UserDTO>>(articles));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        User? user = await _UserService.GetByIdAsync(id);
        if (user == null)
            return NoContent();
        return Ok(_Mapper.Map<UserDTO>(user));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] UserAddDTO userDTO)
    {

        User user = _Mapper.Map<User>(userDTO);
        if (await _UserService.AddAsync(user))
            return Ok();
        return BadRequest();

    }
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCredencials credencials)
    {
        JWToken? token = await _UserService.LoginAsync(credencials);
        if (token == null)
            return BadRequest();
        return Ok(token);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] EditUserDTO userDTO)
    {

        if (await _UserService.UpdateAsync(id, userDTO))
            return Ok();
        return BadRequest();
    }

}
