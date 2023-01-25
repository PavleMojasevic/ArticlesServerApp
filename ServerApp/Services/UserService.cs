using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerApp.Services
{
    public class UserService:IUserService
    { 
        private readonly VoziNaStrujuDbContext _DbContext;
        private readonly IConfigurationSection _SecretKey;
        private readonly IConfigurationSection _TokenAddress;

        public UserService(VoziNaStrujuDbContext dbContext, IConfiguration config)
        {
            _DbContext = dbContext;
            _SecretKey = config.GetSection("SecretKey");
            _TokenAddress = config.GetSection("tokenAddress");
        }

        public async Task<bool> Add(User user)
        {
            _DbContext.Users.Add(user);
            return true;
        }


        public async Task<List<User>> Get()
        {
            return _DbContext.Users.ToList();
        }

        public async Task<User?> GetById(long id)
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
        public async Task<JWToken?> Login(LoginCredencials credencials)
        {
            var user = _DbContext.Users.FirstOrDefault(x => x.Username == credencials.Username && x.Password == Encode(credencials.Password));
            if (user==null)
                return null;
            List<Claim> claims = new List<Claim>(); 
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("role", user.Role.ToString()));   
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SecretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                   issuer: _TokenAddress.Value, 
                   claims: claims, 
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: signinCredentials 
               ); 
            return new JWToken() { Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions) };
        }

        public Task<bool> Update(long id, User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
