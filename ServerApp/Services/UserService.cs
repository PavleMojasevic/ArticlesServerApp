using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerApp.Services;

public class UserService:IUserService
{ 
    private readonly ArticlesDbContext _DbContext;
    private readonly IConfigurationSection _SecretKey;
    private readonly IConfigurationSection _TokenAddress;

    public UserService(ArticlesDbContext dbContext, IConfiguration config)
    {
        _DbContext = dbContext;
        _SecretKey = config.GetSection("SecretKey");
        _TokenAddress = config.GetSection("tokenAddress");
    }

    public async Task<bool> AddAsync(User user)
    {
        _DbContext.Users.Add(user);
        return true;
    }


    public async Task<List<User>> GetAsync()
    {
        return _DbContext.Users.ToList();
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _DbContext.Users.FindAsync(id);
    }

    private const string _pepper = "asfuegwhvgwoe";
    string Encode(string raw)
    {
        using (var sha = SHA256.Create())
        {
            var computedHash = sha.ComputeHash(
            Encoding.Unicode.GetBytes(raw + _pepper));
            return Convert.ToBase64String(computedHash);
        }
    }
    public async Task<JWToken?> LoginAsync(LoginCredencials credencials)
    {
        var user = _DbContext.Users.FirstOrDefault(x => x.Username == credencials.Username && x.Password == Encode(credencials.Password));
        if (user==null)
            return null;
        List<Claim> claims = new List<Claim>(); 
        claims.Add(new Claim("id", user.Id.ToString()));
        claims.Add(new Claim("role", user.Role.ToString()));   
        SymmetricSecurityKey secretKey = new (Encoding.UTF8.GetBytes(_SecretKey.Value));
        SigningCredentials signinCredentials = new (secretKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken tokeOptions = new (
               issuer: _TokenAddress.Value, 
               claims: claims, 
               expires: DateTime.Now.AddDays(1),
               signingCredentials: signinCredentials 
           ); 
        return new () { Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions) };
    }

    public Task<bool> UpdateAsync(long id, User user)
    {
        throw new NotImplementedException();
    } 
}
