using ServerApp.Models;

namespace ServerApp.DTO;

public class UserDTO
{
    public long Id { get; set; }
    public string Username { get; set; } 
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>(); 
}
