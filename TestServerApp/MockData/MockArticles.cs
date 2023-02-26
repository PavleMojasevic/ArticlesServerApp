using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerApp.MockData;

public class MockArticles
{
    private MockUsers _MockUsers;

    public MockArticles()
    {
        _MockUsers = new();
    }

    public List<Article> GetArticles()
    {
        List<User> users = _MockUsers.GetUsersWithoutArticles();

        List<Article> articles = new()
        {
            new ()
            {
                Author=users[0],
                AuthorId=users[0].Id,
                Category=new Category{Id=1, Name="c1"},
                CategoryId=1,
                Comments=new List<Comment>(),
                Date=DateTime.Today,
                Image=null,
                Id=1,
                Tags=new (){ new() { TagName = "tag1", ArticleId=1 },new (){ TagName = "tag2", ArticleId = 1 } },
                Title="title1"
            },
            new ()
            {
                Author=users[0],
                AuthorId=users[0].Id,
                Category=new Category{Id=1, Name="c2"},
                CategoryId=2,
                Comments=new List<Comment>(),
                Date=DateTime.Now.AddDays(-1),
                Image=null,
                Id=2,
                Tags=new (){ new() { TagName = "tag3", ArticleId = 2},new() { TagName = "tag2", ArticleId = 2} },
                Title="title2"
            },

        }; 
        
        return articles;
    }
    public List<Article> GetInvalidArticles()
    {
        List<User> users = _MockUsers.GetUsersWithoutArticles();

        List<Article> articles = new ()
        {
            new ()
            {
                Author=users[0],
                AuthorId=users[0].Id, 
                CategoryId=0,
                Comments=new List<Comment>(),
                Date=DateTime.Today,
                Image=null,
                Id=1,
                Tags=new (){ new () { TagName = "tag1", ArticleId=1 },new (){ TagName = "tag2" , ArticleId=1} },
                Title="title1"
            },
            new ()
            {
                Author=users[0],
                AuthorId=users[0].Id,
                Category=new Category{Id=1, Name="c2"},
                CategoryId=2,
                Comments=new List<Comment>(), 
                Image=null,
                Id=2,
                Tags=new (){ new (){ TagName = "tag3" },new (){ TagName = "tag2" } },
                Title=""
            },

        }; 
        
        return articles;
    }
}
