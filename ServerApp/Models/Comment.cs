namespace ServerApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();
        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }
        public DateTime Date { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
