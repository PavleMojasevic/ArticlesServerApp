namespace ServerApp.Models;

public class ArticleTag
{
    public long ArticleId { get; set; }
    public Article Article { get; set; }
    public string TagName { get; set; } = "";
    public Tag Tag { get; set; }

}
