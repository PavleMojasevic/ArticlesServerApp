using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Interfaces;

public interface IUserService
{
    Task<JWToken?> LoginAsync(LoginCredencials credencials);

    Task<List<User>?> GetAsync();
    Task<User?> GetByIdAsync(long id);
    Task AddAsync(User entity);
    Task<bool> UpdateAsync(long id, EditUserDTO entity);
}
