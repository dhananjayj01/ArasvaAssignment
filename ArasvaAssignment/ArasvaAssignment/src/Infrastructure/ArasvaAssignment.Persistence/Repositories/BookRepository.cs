using ArasvaAssignment.Application.Contracts.Identity;
using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Entities;
using ArasvaAssignment.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ArasvaAssignment.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILoggedInService _loggedInService;

        public BookRepository(ApplicationDbContext applicationDbContext,ILoggedInService loggedInService)
        {
            _applicationDbContext = applicationDbContext;
            _loggedInService = loggedInService;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            book.CreatedBy = _loggedInService.MemberId;  
            book.CreatedOn = DateTime.UtcNow;

            await _applicationDbContext.Books.AddAsync(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync(
            string? search = null,
            bool? isAvailable = null)
        {
            IQueryable<Book> query = _applicationDbContext.Books
                .Where(b => !b.IsDeleted);

            // Availability filter
            if (isAvailable.HasValue)
            {
                query = query.Where(b => b.IsAvailable == isAvailable.Value);
            }

            // Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                string keyword = search.Trim().ToLower();

                query = query.Where(b =>
                    b.Title.ToLower().Contains(keyword) ||
                    b.Author.ToLower().Contains(keyword) ||
                    b.ISBN.ToString().Contains(keyword)
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Book?> GetBookById(Guid bookId)
        {
            return await _applicationDbContext.Books
                .FirstOrDefaultAsync(b => b.Id == bookId && !b.IsDeleted);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            book.ModifiedBy = _loggedInService.MemberId;
            book.ModifiedOn = DateTime.UtcNow;

            _applicationDbContext.Books.Update(book);
            await _applicationDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> IsBookExistsAsync(
            string title,
            int isbn,
            Guid? excludeBookId = null)
        {
            return await _applicationDbContext.Books.AnyAsync(b =>
                !b.IsDeleted &&
                (b.Title == title || b.ISBN == isbn) &&
                (excludeBookId == null || b.Id != excludeBookId)
            );
        }
    }
}
