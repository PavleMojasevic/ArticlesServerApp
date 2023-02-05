using ServerApp.Models;

namespace ServerApp.DTO
{
    public class ArticleFilterDTO
    {
        public long? CategoryId { get; set; }
        public long? TagId { get; set; }
        public long? AuthorId{ get; set; }
    }
}
