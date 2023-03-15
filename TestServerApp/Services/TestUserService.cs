using Microsoft.Extensions.Configuration;
using MockQueryable.Moq;
using Moq;
using ServerApp.Infrastucture;
using ServerApp.Models;
using ServerApp.Services;
using TestServerApp.MockData;

namespace TestServerApp.Services;


public class TestUserService
{


    private readonly Mock<Microsoft.EntityFrameworkCore.DbSet<User>> _DbSet;
    private readonly MockUsers _MockUsers = new();
    private readonly Mock<ArticlesDbContext> _MockDbContext;
    private readonly UserService _UserService;
    public TestUserService()
    {
        _DbSet = _MockUsers.GetUsers()
            .BuildMock()
            .BuildMockDbSet();
        _MockDbContext = new();
        _MockDbContext.Setup(x => x.Users)
                     .Returns(_DbSet.Object);
        Mock<IConfiguration> configuration = new();
        _UserService = new(_MockDbContext.Object, configuration.Object);
    }

    /* [Theory]
    [InlineData(-1, false)]
    [InlineData(-2, false)]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(33, false)]
   public async Task Add_TestAsync(long id, bool expected)
    {
        Article? article;
        article = (id > 0) ? _MockUsers.GetUsers().FirstOrDefault(x => x.Id == id) :
                             _MockUsers.get().FirstOrDefault(x => x.Id == -id);

        if (article == null)
        {
            Assert.False(expected);
            return;
        }
        var mock = new List<Article> { new() { Title = "title1" } }
           .BuildMock().BuildMockDbSet();

        var mockDbContext = new Mock<ArticlesDbContext>();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(mock.Object);

        ArticleService articleService = new(mockDbContext.Object);

        try
        {
            await articleService.AddAsync(article);
            Assert.True(expected);

        }
        catch (ArgumentException)
        {
            Assert.False(expected);
        }

    }*/

    [Fact]
    public async Task Get_ShouldReturnArticlesAsync()
    {
        var employee = await _UserService.GetAsync();
        Assert.NotNull(employee);
        Assert.True(employee.Count == _DbSet.Object.Count());
    }
    [Fact]
    public async Task AddUserAsync()
    {
        await _UserService.AddAsync(_MockUsers.GetUsers().First());
    }
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task LoginAsync(bool validToken)
    {
        LoginCredencials? credencials = new();
        if (validToken)
            credencials = new()
            {
                Username = "user1",
                Password = "user1"
            };
        var token = await _UserService.LoginAsync(credencials);
        Assert.Equal(validToken, token != null);
    }
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(-4, false)]
    public async Task GetById_HasValueAsync(long id, bool hasValue)
    {
        var result = await _UserService.GetByIdAsync(id);
        if (hasValue)
            Assert.True(result != null);
        else
            Assert.False(result != null);

    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(4, false)]
    public async Task Put_TestAsync(long id, bool hasValue)
    {
        Assert.True(await _UserService.UpdateAsync(id, _MockUsers.GetEditUserDto()) == hasValue);
    }
}
