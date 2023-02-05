using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServerApp.Controllers;
using ServerApp.Infrastucture;
using ServerApp.Interfaces;
using ServerApp.Models;
using ServerApp.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestServerApp.MockData;

namespace TestServerApp.Services;


public class TestArticleService
{

  /*  [Theory]
    [InlineData(-1, false)]
    [InlineData(-2, false)]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    public async Task Add_Test(long id, bool expected)
    {
        MockArticles mockArticles = new ();
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
        var data = mockArticles.GetArticles().AsQueryable();

        var mockSet = new Mock<DbSet<Article>>();
        mockSet.As<IDbAsyncEnumerable<Article>>()
        .Setup(m => m.GetAsyncEnumerator())
            .Returns(new TestDbAsyncEnumerator<Article>(data.GetEnumerator()));

        mockSet.As<IQueryable<Article>>()
              .Setup(m => m.Provider)
              .Returns(new TestDbAsyncQueryProvider<Article>(data.Provider));


        mockSet.As<IQueryable<Article>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Article>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Article>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        var mockContext = new Mock<ArticlesDbContext>();
        mockContext.Setup(c => c.Articles).Returns(mockSet.Object);

        var service = new ArticleService(mockContext.Object);
        var articles = await service.GetAsync();

        Assert.AreEqual(3, articles.Count);
        Assert.AreEqual("AAA", articles[0].Name);
        Assert.AreEqual("BBB", articles[1].Name);
        Assert.AreEqual("ZZZ", articles[2].Name);
        var dbContext = new Mock<ArticlesDbContext>();
        MockUsers mockUsers = new();
        dbContext.Setup(x => x.Articles).Returns(;
        var sut = new ArticleService(dbContext.Object);

        var result = await sut.AddAsync(article);
        Assert.True(expected==result);

    }*/
}
