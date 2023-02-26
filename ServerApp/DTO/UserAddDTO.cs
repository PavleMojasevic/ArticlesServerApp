using ServerApp.Models;

namespace ServerApp.DTO
{
    public class UserAddDTO
    { 
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public UserRole Role { get; set; } 
    }
}
