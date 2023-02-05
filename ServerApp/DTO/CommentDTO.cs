namespace ServerApp.DTO;

public class CommentDTO
{
    public long Id { get; set; }
    public string AuthorUsername { get; set; }
    public long AuthorId { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public List<CommentDTO> Replies { get;set; }=new List<CommentDTO>();
    public DateTime Date { get; set; }

}
