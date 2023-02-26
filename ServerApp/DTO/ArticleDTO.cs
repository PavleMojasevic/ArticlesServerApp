namespace ServerApp.DTO;

public class ArticleDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public byte[]? Image { get; set; }
    public string AuthorName { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>(); 
    public DateTime Date { get; set; }
    public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
}
