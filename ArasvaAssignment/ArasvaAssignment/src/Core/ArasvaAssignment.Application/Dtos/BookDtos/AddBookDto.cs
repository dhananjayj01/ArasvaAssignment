namespace ArasvaAssignment.Application.Dtos.BookDtos
{
    public class AddBookDto
    {
        public string Title { get; set; }                 
        public int ISBN { get; set; }                       
        public string? Category { get; set; }             
        public string Author { get; set; }                
        public string? Publisher { get; set; }           
        public bool IsAvailable { get; set; } = true;     
    }
}
