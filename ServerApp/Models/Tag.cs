namespace ServerApp.Models;

public class Tag
{ 
    public string Name { get; set; } = "";
    public List<ArticleTag> Articles { get; set; }=new List<ArticleTag>();    
}
