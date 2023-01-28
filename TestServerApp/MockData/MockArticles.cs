﻿using ServerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerApp.MockData
{
    public class MockArticles
    {
        private MockUsers _MockUsers=new MockUsers();

        public MockArticles()
        { 
        }

        public List<Article> GetArticles()
        {
            List<User> users = _MockUsers.GetUsersWithoutArticles();

            List<Article> articles = new List<Article>
            {
                new Article
                {
                    Author=users[0],
                    AuthorId=users[0].Id,
                    Category=new Category{Id=1, Name="c1"},
                    CategoryId=1,
                    Comments=new List<Comment>(),
                    Date=DateTime.Today,
                    Image=null,
                    Id=1,
                    Tags=new List<Tag>{ new Tag { Name = "tag1" },new Tag{ Name = "tag2" } },
                    Title="title1"
                },
                new Article
                {
                    Author=users[0],
                    AuthorId=users[0].Id,
                    Category=new Category{Id=1, Name="c2"},
                    CategoryId=2,
                    Comments=new List<Comment>(),
                    Date=DateTime.Now.AddDays(-1),
                    Image=null,
                    Id=2,
                    Tags=new List<Tag>{ new Tag { Name = "tag3" },new Tag{ Name = "tag2" } },
                    Title="title2"
                },

            }; 
            
            return articles;
        }
    }
}