namespace ServerApp.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>(); 
        public DateTime Date { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
 