using MockQueryable.Moq;
using Moq;
using ServerApp.Infrastucture;
using ServerApp.Models;
using ServerApp.Services;
using TestServerApp.MockData;

namespace TestServerApp.Services;


public class TestCategoryService
{


    private readonly Mock<Microsoft.EntityFrameworkCore.DbSet<Category>> _DbSet;
    private readonly MockCategory _MockCategories = new();
    private readonly Mock<ArticlesDbContext> _MockDbContext;
    private readonly CategoryService _CategoryService;
    public TestCategoryService()
    {
        _DbSet = _MockCategories.GetCategories()
            .BuildMock()
            .BuildMockDbSet();
        _MockDbContext = new();
        _MockDbContext.Setup(x => x.Categories)
                     .Returns(_DbSet.Object);
        _CategoryService = new(_MockDbContext.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnArticlesAsync()
    {
        var employee = await _CategoryService.GetAsync();
        Assert.NotNull(employee);
        Assert.True(employee.Count == _DbSet.Object.Count());
    }
    [Theory]
    [InlineData("Category1", null, false)]
    [InlineData("Category3", null, true)]
    [InlineData("Category3", (long)1, true)]
    [InlineData("Category3", (long)11, false)]
    public async Task AddCategoryAsync(string name, long? parent, bool isValid)
    {
        Assert.Equal(isValid, await _CategoryService.AddAsync(new() { Name = name, ParentId = parent }));

    }
    [Theory]
    [InlineData(11, "Category1", null, false)]
    [InlineData(1, "Category3", null, true)]
    [InlineData(1, "Category3", (long)1, true)]
    [InlineData(1, "Category3", (long)11, false)]
    public async Task EditCategoryAsync(long id, string name, long? parent, bool isValid)
    {
        Assert.Equal(isValid, await _CategoryService.EditAsync(id, new() { Name = name, ParentId = parent }));
    }
    [Fact]
    public async Task GetAsync()
    {
        var result = await _CategoryService.GetAsync();
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

    }
}
