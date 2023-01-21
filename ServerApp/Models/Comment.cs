namespace ServerApp.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
        public List<Comment> Replies { get; set; } = new List<Comment>();
        public long? ParentId { get; set; }
        public Comment? Parent { get; set; }
        public DateTime Date { get; set; }
        public long ArticleId { get; set; }
        public Article Article { get; set; }

        public long AuthorId { get; set; }
        public User Author { get; set; }
    }
}
