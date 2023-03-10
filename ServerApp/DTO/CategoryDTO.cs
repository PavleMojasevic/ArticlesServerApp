namespace ServerApp.DTO;

public class CategoryDTO
{ 
    public string Name { get; set; } = "";
    public long Id { get; set; }
    public long? ParentId { get; set; }
}
