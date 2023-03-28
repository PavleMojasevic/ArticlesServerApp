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


    [Fact]
    public async Task Get_ShouldReturnUsersAsync()
    {
        var users = await _UserService.GetAsync();
        Assert.NotNull(users);
        Assert.True(users.Count == _DbSet.Object.Count());
    }
    [Fact]
    public async Task AddUserAsync()
    {
        await _UserService.AddAsync(_MockUsers.GetUsers().First());
    }
    [Fact]
    public async Task AddUser_ThrowsExceptionAsync()
    {
        try
        {

            await _UserService.AddAsync(_MockUsers.GetUsers().First());
        }
        catch (Exception)
        {
            Assert.True(true);
        }
        Assert.True(false);
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
