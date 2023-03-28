using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.DTO;
using ServerApp.Interfaces;
using ServerApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // GET: api/<UsersController>
    private readonly IUserService _UserService;
    private readonly IMapper _Mapper;
    public UserController(IUserService userService, IMapper mapper)
    {
        _UserService = userService;
        _Mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
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
        await _UserService.AddAsync(user);
        return Ok();

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
