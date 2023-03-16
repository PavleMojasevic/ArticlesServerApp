using ServerApp.DTO;
using ServerApp.Models;

namespace TestServerApp.MockData;

public class MockComment
{
    public List<CommentDTO> GetCommentsDTO()
    {
        return new List<CommentDTO>();
    }
    public List<Comment> GetComments()
    {
        return new()
        {
            new(){Id=1, AuthorId=1, ArticleId=2, Text="comment1", Status=CommentStatus.Approved},
            new(){Id=2, AuthorId=1, ArticleId=1, Text="comment2", Status=CommentStatus.Approved},
            new(){Id=3, AuthorId=1, ArticleId=1, Text="comment3", Status=CommentStatus.Undefined}
        };
    }
}
