using ArasvaAssignment.Application.Contracts.Identity;
using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Domain.Entities;
using ArasvaAssignment.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using static ArasvaAssignment.Application.Dtos.BorrowTransactionDtos.BorrowingHistoryDto;

namespace ArasvaAssignment.Persistence.Repositories
{
    public class BorrowTransactionRepository : IBorrowTransactionsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILoggedInService _loggedInService;

        public BorrowTransactionRepository(ApplicationDbContext applicationDbContext,ILoggedInService loggedInService)
        {
            _applicationDbContext = applicationDbContext;
            _loggedInService = loggedInService;
        }

        // Check if book is already borrowed
        public async Task<bool> IsBookBorrowed(Guid bookId)
        {
            return await _applicationDbContext.BorrowTransactions
                .AnyAsync(b =>
                    b.BookId == bookId &&
                    b.ReturnDate == null
                );
        }

        // Borrow Book
        public async Task<BorrowTransactions> BorrowBookAsync(BorrowTransactions borrow)
        {
            borrow.BorrowDate = DateTime.UtcNow;
            borrow.DueDate = borrow.BorrowDate.AddDays(7);
            borrow.CreatedOn = DateTime.UtcNow;
            borrow.CreatedBy = _loggedInService.MemberId;

            await _applicationDbContext.BorrowTransactions.AddAsync(borrow);

            // Update book availability
            var book = await _applicationDbContext.Books.FindAsync(borrow.BookId);
            if (book != null)
            {
                book.IsAvailable = false;
            }

            await _applicationDbContext.SaveChangesAsync();
            return borrow;
        }

        // Get transaction by Id
        public async Task<BorrowTransactions?> GetBorrowTransaction(Guid id)
        {
            return await _applicationDbContext.BorrowTransactions
                .Include(x => x.Book)
                .Include(x => x.Member)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Return book
        public async Task<bool> ReturnBookAsync(BorrowTransactions transaction)
        {
            var trx = await _applicationDbContext.BorrowTransactions
                .FirstOrDefaultAsync(x => x.Id == transaction.Id);

            if (trx == null)
                return false;

            trx.ReturnDate = transaction.ReturnDate ?? DateTime.UtcNow;
            trx.ModifiedOn = DateTime.UtcNow;
            trx.ModifiedBy = _loggedInService.MemberId;

            var book = await _applicationDbContext.Books.FindAsync(trx.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }

            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        // Borrow history by member
        public async Task<List<BorrowHistoryDto>> GetBorrowingHistoryByMemberId(Guid memberId)
        {
            return await _applicationDbContext.BorrowTransactions
                .Where(x => x.MemberId == memberId)
                .Include(x => x.Book)
               .Select(x => new BorrowHistoryDto
               {
                   TransactionId = x.Id,
                   BookTitle = x.Book.Title,
                   BorrowDate = x.BorrowDate,
                   DueDate = x.DueDate,
                   ReturnDate = x.ReturnDate,

                   IsReturned = x.ReturnDate != null    
               })

                .ToListAsync();
        }
    }
}
