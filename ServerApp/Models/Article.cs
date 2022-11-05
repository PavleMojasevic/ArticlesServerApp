namespace ServerApp.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Content> Content { get; set; } = new List<Content>();
        public DateTime Date { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
