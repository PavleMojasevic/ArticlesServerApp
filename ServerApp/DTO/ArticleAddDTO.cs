namespace ServerApp.DTO;

public class ArticleAddDTO
{
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public byte[]? Image { get; set; }
    public long CategoryId { get; set; } = 0;
    public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
}
