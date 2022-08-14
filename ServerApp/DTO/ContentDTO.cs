namespace ServerApp.DTO
{
    public enum ContentType { TEXT, ITALIC, BOLD, IMAGE }
    public class ContentDTO
    {
        public ContentType type { get; set; }
        public string Text { get; set; }
    }
}
