namespace ServerApp.Models;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = "";    
    public long? ParentId { get; set; }
    public Category? Parent { get; set; }
    public List<Category> Subcategories { get; set; } = new List<Category>();
    public List<Article> Articles { get; set; } = new List<Article>();
}
