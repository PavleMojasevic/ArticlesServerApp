﻿using Moq;
using ServerApp.Infrastucture;
using ServerApp.Models;
using ServerApp.Services;
using TestServerApp.MockData;
using MockQueryable.Moq;
using System.Data.Entity;
using ServerApp.DTO;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Controllers;
using ServerApp.Interfaces;

namespace TestServerApp.Services;


public class TestUserService
{ 
    [Theory]
    [InlineData(-1, false)]
    [InlineData(-2, false)]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(33, false)]
    public async Task Add_Test(long id, bool expected)
    {
        MockArticles mockArticles = new();
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
            Assert.False(expected);
            return;
        }
        var mock = new List<Article>()
           .BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ArticlesDbContext>();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(mock.Object);

        ArticleService articleService = new(mockDbContext.Object);
        await articleService.AddAsync(article); 
    }

    [Fact]
    public async Task Get_ShouldReturnArticlesAsync()
    {
        MockArticles mockArticles = new(); 
        var mock = mockArticles.GetArticles()
            .BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ArticlesDbContext>();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(mock.Object);


        ArticleService articleService = new(mockDbContext.Object);
        var filter = new ArticleFilterDTO
        {
            CategoryId= 1,
            AuthorId= 1,
            Tag = null
        };
        
        var employee = await articleService.GetAsync();
        Assert.NotNull(employee); 
        Assert.True(employee.Count==mock.Object.Count());
        

        employee = await articleService.GetAsync(filter);
        Assert.NotNull(employee); 
        Assert.True(employee.Count<=mock.Object.Count()); 
    }
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(-4, false)]
    public async Task GetById_HasValueAsync(long id, bool hasValue)
    {
        MockArticles mockArticles = new();
        var mock = mockArticles.GetArticles()
            .BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ArticlesDbContext>();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(mock.Object);
        ArticleService articleService = new(mockDbContext.Object);
        if(hasValue)
            Assert.True(await articleService.GetByIdAsync(id)!=null);
        else
            Assert.False(await articleService.GetByIdAsync(id) != null);
         
    }

    [Theory]
    [InlineData(1,0, true)]
    [InlineData(1,1, true)]
    [InlineData(1,2, true)]
    [InlineData(1,3, true)]
    [InlineData(1,4, true)]
    [InlineData(2,2, true)]
    [InlineData(3,3, false)]
    [InlineData(-4,1, false)]
    public async Task Put_TestAsync(long id,int index, bool hasValue)
    {
        MockArticles mockArticles = new();
        var mock = mockArticles.GetArticles()
            .BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ArticlesDbContext>();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(mock.Object);
        ArticleService articleService = new(mockDbContext.Object);
        //TODO
         

        Assert.True(await articleService.UpdateAsync(id, mockArticles.GetEditArticeDto()[index]) == hasValue);

    }
}
