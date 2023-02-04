using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerApp.MockData
{
    public class MockUsers
    {
        private List<User> _Users = new List<User>
        {
            GetUser(1,"user1", UserRole.AUTHOR),
            GetUser(2,"user2", UserRole.ADMIN),
            GetUser(3,"user3", UserRole.READER, new List<Comment>{ new Comment ("text", 1) }),
             
        };
        public List<User> GetUsersWithoutArticles()
        {
            List<User> users = _Users.Select(x=>(User)x.Clone()).ToList();
            users.ForEach(x => x.Articles = new List<Article>());
            return _Users;
        }
        public List<User> GetUsers()
        { 
            return _Users;
        }
        private static User GetUser(long id, string username, UserRole role, List<Comment>?comments=null)
        {
            if(comments==null) comments = new List<Comment>();
            comments.ForEach(x=>x.AuthorId=id);
            User user = new User
            {
                Id = id,
                Created = DateTime.Today.AddDays(-10),
                Comments = comments,
                Articles = new List<Article>(),
                Email = $"{username}@mail.com",
                Password = "password" + id,
                Role = role,
                Username = username
            };
            return user;
        }
    }
}
