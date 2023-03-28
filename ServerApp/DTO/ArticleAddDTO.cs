namespace ServerApp.DTO;

public class ArticleAddDTO
{
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string? Image { get; set; }
    public long CategoryId { get; set; } = 0;
}
