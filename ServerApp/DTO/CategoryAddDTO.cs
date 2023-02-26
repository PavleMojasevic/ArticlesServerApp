namespace ServerApp.DTO
{
    public class CategoryAddDTO
    {
        public string Name { get; set; } = "";
        public long? ParentId { get; set; }
    }
}
