using ServerApp.DTO;
using ServerApp.Models;

namespace TestServerApp.MockData;

public class MockUsers
{
    private List<User> _Users = new List<User>
    {
        GetUser(1,"user1","7PdGGtoSal85KyZaP7/ZiNgp2daz691R+jb6h87hMu4=", UserRole.AUTHOR),
        GetUser(2,"user2","prLcxdx+OEsn4R0gGVcd2fAg9Y+vtTl8u9H63mGWLLk=", UserRole.ADMIN),
        GetUser(3,"user3","s9gsHKwlymiPuo5/Evuz3ot7mWNIshzwMgvhUqleQcg=", UserRole.READER, new List<Comment>{ new Comment ("text", 1) }),

    };
    public List<User> GetUsersWithoutArticles()
    {
        List<User> users = _Users.Select(x => x.Clone()).ToList();
        users.ForEach(x => x.Articles = new List<Article>());
        return _Users;
    }
    public List<User> GetUsers()
    {
        return _Users;
    }
    private static User GetUser(long id, string username, string password, UserRole role, List<Comment>? comments = null)
    {
        if (comments == null) comments = new();
        comments.ForEach(x => x.AuthorId = id);
        User user = new()
        {
            Id = id,
            Created = DateTime.Today.AddDays(-10),
            Comments = comments,
            Articles = new List<Article>(),
            Email = $"{username}@mail.com",
            Password = password,
            Role = role,
            Username = username,
        };
        return user;
    }
    public EditUserDTO GetEditUserDto()
    {
        return new();
    }
}
