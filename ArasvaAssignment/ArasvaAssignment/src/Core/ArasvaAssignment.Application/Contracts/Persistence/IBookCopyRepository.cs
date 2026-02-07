using ArasvaAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface IBookCopyRepository
    {
        Task<BookCopy> AddBookCopyAsync(BookCopy bookCopy);
        Task<IEnumerable<BookCopy>> GetAllBookCopyAsync(string? search = null, bool? isAvailable = null);
        Task<BookCopy> UpdateBookCopyAsync(BookCopy bookCopy);
        Task<BookCopy?> GetBookCopyById(Guid copyId);
        Task<bool> IsBookCopyExistsAsync(string barcode, Guid? excludeBookCopyId = null);
    }
}
