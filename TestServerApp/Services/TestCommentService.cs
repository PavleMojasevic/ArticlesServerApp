using MockQueryable.Moq;
using Moq;
using ServerApp.Infrastucture;
using ServerApp.Models;
using ServerApp.Services;
using TestServerApp.MockData;

namespace TestServerApp.Services;


public class TestCommentService
{
    private readonly Mock<Microsoft.EntityFrameworkCore.DbSet<Comment>> _DbSetComments;
    private readonly Mock<Microsoft.EntityFrameworkCore.DbSet<User>> _DbSetUsers;
    private readonly MockComment _MockComment = new();
    private readonly MockUsers _MockUser = new();
    private readonly Mock<ArticlesDbContext> _MockDbContext;
    private readonly CommentService _CommentService;
    public TestCommentService()
    {
        _DbSetComments = _MockComment.GetComments()
            .BuildMock()
            .BuildMockDbSet();
        _DbSetUsers = _MockUser.GetUsers()
            .BuildMock()
            .BuildMockDbSet();
        _MockDbContext = new();
        _MockDbContext.Setup(x => x.Comments)
                     .Returns(_DbSetComments.Object);
        _MockDbContext.Setup(x => x.Users)
                     .Returns(_DbSetUsers.Object);
        _CommentService = new(_MockDbContext.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnCommentsAsync()
    {
        var comments = await _CommentService.GetAsync(1);
        Assert.NotNull(comments);
        Assert.Equal(2, comments.Count);
    }
    [Fact]
    public async Task GetByArticle_ShouldReturnCommentsAsync()
    {
        var comments = await _CommentService.GetByArticleAsync(1, 1);
        Assert.NotNull(comments);
        Assert.Equal(2, comments.Count);
    }
    [Fact]
    public async Task GetAll_ShouldReturnCommentsAsync()
    {
        var comments = await _CommentService.GetAllAsync();
        Assert.NotNull(comments);
        Assert.Equal(3, comments.Count);
    }
    /* [Theory]
     [InlineData("Category1", null, false)]
     [InlineData("Category3", null, true)]
     [InlineData("Category3", (long)1, true)]
     [InlineData("Category3", (long)11, false)]
     public async Task AddCategoryAsync(string name, long? parent, bool isValid)
     {
         Assert.Equal(isValid, await _CommentService.AddAsync(new() { Name = name, ParentId = parent }));

     }
     [Theory]
     [InlineData(11, "Category1", null, false)]
     [InlineData(1, "Category3", null, true)]
     [InlineData(1, "Category3", (long)1, true)]
     [InlineData(1, "Category3", (long)11, false)]
     public async Task EditCategoryAsync(long id, string name, long? parent, bool isValid)
     {
         Assert.Equal(isValid, await _CommentService.EditAsync(id, new() { Name = name, ParentId = parent }));
     }*/
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task AddAsync(bool validComment)
    {
        var comment = _MockComment.GetComments().First();
        if (!validComment)
            comment = new();
        Assert.Equal(validComment, await _CommentService.AddAsync(comment));

    }
    [Theory]
    [InlineData(true, 3)]
    [InlineData(false, 11)]
    [InlineData(false, 1)]
    public async Task ApproveAsync(bool validComment, long id)
    {
        Assert.Equal(validComment, await _CommentService.ApproveAsync(id));

    }
    [Theory]
    [InlineData(true, 3)]
    [InlineData(false, 11)]
    [InlineData(false, 1)]
    public async Task RejectAsync(bool validComment, long id)
    {
        Assert.Equal(validComment, await _CommentService.RejectAsync(id));

    }
    [Fact]
    public async Task LikeTestAsync()
    {
        var comment = _MockComment.GetComments().First(x => x.Id == 1);
        Assert.True(await _CommentService.AddLikeAsync(1, 1));
        Assert.False(await _CommentService.AddLikeAsync(1, 1));
        Assert.True(await _CommentService.RemoveLikeAsync(1, 1));
        Assert.False(await _CommentService.RemoveLikeAsync(1, 1));
        Assert.True(await _CommentService.AddLikeAsync(1, 1));
        Assert.True(await _CommentService.AddDislikeAsync(1, 1));
    }
    [Fact]
    public async Task DislikeTestAsync()
    {
        var comment = _MockComment.GetComments().First(x => x.Id == 1);
        Assert.True(await _CommentService.AddDislikeAsync(1, 1));
        Assert.True(await _CommentService.RemoveDislikeAsync(1, 1));
        Assert.False(await _CommentService.RemoveDislikeAsync(1, 1));
        Assert.True(await _CommentService.AddDislikeAsync(1, 1));
        Assert.False(await _CommentService.AddDislikeAsync(1, 1));
        Assert.True(await _CommentService.AddLikeAsync(1, 1));
    }
    [Fact]
    public async Task Like_InvalidAsync()
    {
        var comment = _MockComment.GetComments().First(x => x.Id == 1);
        Assert.False(await _CommentService.AddLikeAsync(11, 1));
        Assert.False(await _CommentService.AddLikeAsync(1, 11));
        Assert.False(await _CommentService.RemoveLikeAsync(11, 1));
        Assert.False(await _CommentService.RemoveLikeAsync(1, 11));
    }
    [Fact]
    public async Task Dislike_InvalidAsync()
    {
        var comment = _MockComment.GetComments().First(x => x.Id == 1);
        Assert.False(await _CommentService.AddDislikeAsync(11, 1));
        Assert.False(await _CommentService.AddDislikeAsync(1, 11));
        Assert.False(await _CommentService.RemoveDislikeAsync(11, 1));
        Assert.False(await _CommentService.RemoveDislikeAsync(1, 11));
    }

}
