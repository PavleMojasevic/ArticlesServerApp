namespace ServerApp.DTO
{
    public class CommentDTO
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<CommentDTO> Replies { get;set; }=new List<CommentDTO>();
        public DateTime Date { get; set; }

    }
}
