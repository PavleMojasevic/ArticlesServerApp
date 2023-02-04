using ServerApp.Models;

namespace ServerApp.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<JWToken?> LoginAsync(LoginCredencials credencials);
    }
}
