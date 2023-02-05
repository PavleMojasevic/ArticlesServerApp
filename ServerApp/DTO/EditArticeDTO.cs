namespace ServerApp.DTO
{
    public class EditArticeDto
    {

        public string? Title { get; set; }
        public string? Content { get; set; }
        public byte[]? Image { get; set; }
        public long CategoryId { get; set; }
    }
}
