using ArasvaAssignment.Domain.Entities; 

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book); 
        Task<IEnumerable<Book>> GetAllBookAsync( string? search = null,  bool? isAvailable = null);
        Task<Book> UpdateBookAsync(Book book); 
        Task<Book?> GetBookById(Guid bookId); 
        Task<bool> IsBookExistsAsync( string title, int isbn, Guid? excludeBookId = null);
    }
}
