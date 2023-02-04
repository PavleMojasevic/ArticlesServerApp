namespace ServerApp.Models;

public class Tag
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Article> Articles { get; set; }=new List<Article>();    
}
