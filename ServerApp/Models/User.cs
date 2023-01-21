namespace ServerApp.Models
{
    public enum UserRole { AUTHOR, READER, ADMIN}
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
