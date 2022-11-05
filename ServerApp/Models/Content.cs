namespace ServerApp.Models
{
    public enum ContentType { TEXT, ITALIC, BOLD, IMAGE }
    public class Content
    {
        public int Id { get; set; }
        public ContentType type { get; set; }
        public string Text { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
