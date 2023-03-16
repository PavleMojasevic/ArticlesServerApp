using MockQueryable.Moq;
using Moq;
using ServerApp.DTO;
using ServerApp.Infrastucture;
using ServerApp.Models;
using ServerApp.Services;
using TestServerApp.MockData;

namespace TestServerApp.Services;


public class TestArticleService
{

    private readonly Mock<Microsoft.EntityFrameworkCore.DbSet<Article>> _DbSet;
    private readonly MockArticles mockArticles = new();
    private readonly Mock<ArticlesDbContext> mockDbContext;
    private readonly ArticleService articleService;
    public TestArticleService()
    {
        _DbSet = mockArticles.GetArticles()
            .BuildMock()
            .BuildMockDbSet();
        mockDbContext = new();
        mockDbContext.Setup(x => x.Articles)
                     .Returns(_DbSet.Object);
        articleService = new(mockDbContext.Object);
    }

    [Theory]
    [InlineData(-1, false)]
    [InlineData(-2, false)]
    [InlineData(2, true)]
    [InlineData(2, false)]
    public async Task Add_TestAsync(long id, bool expected)
    {
        Article article = id > 0
            ? mockArticles.GetArticles().First(x => x.Id == id)
            : mockArticles.GetInvalidArticles().First(x => x.Id == -id);
        if (expected)
        {
            article.Title = "newTitle";
        }
        try
        {
            await articleService.AddAsync(article);
            Assert.True(expected);

        }
        catch (ArgumentException)
        {
            Assert.False(expected);
        }

    }

    [Fact]
    public async Task Get_ShouldReturnArticlesAsync()
    {

        var filter = new ArticleFilterDTO
        {
            CategoryId = 1,
            AuthorId = 1,
            Tag = "tag"
        };

        var employee = await articleService.GetAsync();
        Assert.NotNull(employee);
        Assert.True(employee.Count == _DbSet.Object.Count());


        employee = await articleService.GetAsync(filter);
        Assert.NotNull(employee);
        Assert.True(employee.Count <= _DbSet.Object.Count());
    }
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, true)]
    [InlineData(3, false)]
    [InlineData(-4, false)]
    public async Task GetById_HasValueAsync(long id, bool hasValue)
    {
        if (hasValue)
            Assert.True(await articleService.GetByIdAsync(id) != null);
        else
            Assert.False(await articleService.GetByIdAsync(id) != null);

    }

    [Theory]
    [InlineData(1, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 2, true)]
    [InlineData(1, 3, true)]
    [InlineData(1, 4, true)]
    [InlineData(2, 2, true)]
    [InlineData(3, 3, false)]
    [InlineData(-4, 1, false)]
    public async Task Put_TestAsync(long id, int index, bool hasValue)
    {
        Assert.True(await articleService.UpdateAsync(id, mockArticles.GetEditArticeDto()[index]) == hasValue);
    }
}
