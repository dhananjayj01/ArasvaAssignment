namespace ArasvaAssignment.Application.Dtos.BookDtos
{
    public class BookDto
    {
        public Guid Id { get; set; }                       
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string? Category { get; set; }
        public string Author { get; set; }
        public string? Publisher { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }                
    }
}
