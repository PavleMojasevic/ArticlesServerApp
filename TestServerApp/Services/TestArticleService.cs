using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServerApp.Controllers;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;
using ServerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestServerApp.MockData;

namespace TestServerApp.Services
{

    public class TestArticleService
    {

        [Theory]
        [InlineData(-1, false)]
        [InlineData(-2, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        public async Task Add_Test(long id, bool expected)
        {
            MockArticles mockArticles = new MockArticles();
            Article? article;
            if (id > 0)
            {
                article = mockArticles.GetArticles().FirstOrDefault(x => x.Id == id);
            }
            else
            {
                article = mockArticles.GetInvalidArticles().FirstOrDefault(x => x.Id == -id);
            }
            if (article == null)
            {
                Assert.True(!expected);
                return;
            }

            var dbContext = new Mock<ArticlesDbContext>();
            MockUsers mockUsers = new MockUsers();
            dbContext.Setup(x => x.Articles.AddAsync(It.IsAny<Article>(), CancellationToken.None));
            var sut = new ArticleService(dbContext.Object);

            var result = await sut.Add(article);
            Assert.True(expected==result);

        }
    }
}
