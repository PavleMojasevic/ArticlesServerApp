namespace ServerApp.DTO;

public class ArticleFilterDTO
{
    public long? CategoryId { get; set; }
    public string? Tag { get; set; }
    public long? AuthorId { get; set; }
}
