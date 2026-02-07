using ArasvaAssignment.Application.Contracts.Identity;
using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Entities;
using ArasvaAssignment.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Persistence.Repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggedInService _loggedInService;

        public BookCopyRepository(ApplicationDbContext context, ILoggedInService loggedInService)
        {
            _context = context;
            _loggedInService = loggedInService;
        }

        public async Task<BookCopy> AddBookCopyAsync(BookCopy bookCopy)
        {
            await _context.BookCopys.AddAsync(bookCopy);
            await _context.SaveChangesAsync();
            return bookCopy;
        }

        public async Task<IEnumerable<BookCopy>> GetAllBookCopyAsync(
            string? search = null,
            bool? isAvailable = null)
        {
            IQueryable<BookCopy> query = _context.BookCopys
                .Where(b => !b.IsDeleted);

            // Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                string keyword = search.Trim().ToLower();

                query = query.Where(b =>
                    b.Barcode.ToLower().Contains(keyword) ||
                    b.Status.ToString().ToLower().Contains(keyword) 
                );
            }
            return await query.ToListAsync();
        }

        public async Task<BookCopy?> GetBookCopyById(Guid copyId)
        {
            return await _context.BookCopys
                .FirstOrDefaultAsync(b => b.Id == copyId && !b.IsDeleted);
        }

        public async Task<BookCopy> UpdateBookCopyAsync(BookCopy bookCopy)
        {
            bookCopy.ModifiedBy = _loggedInService.MemberId;
            bookCopy.ModifiedOn = DateTime.UtcNow;

            _context.BookCopys.Update(bookCopy);
            await _context.SaveChangesAsync();
            return bookCopy;
        }

        public async Task<bool> IsBookCopyExistsAsync(
            string barcode,
            Guid? excludeBookCopyId = null)
        {
            return await _context.BookCopys.AnyAsync(b =>
                !b.IsDeleted &&
                (b.Barcode == barcode) &&
                (excludeBookCopyId == null || b.Id != excludeBookCopyId)
            );
        }
    }
}
