using ArasvaAssignment.Domain.Entities;
using static ArasvaAssignment.Application.Dtos.BorrowTransactionDtos.BorrowingHistoryDto;

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface IBorrowTransactionsRepository
    { 
        Task<bool> IsBookBorrowed(Guid bookId); 
        Task<BorrowTransactions> BorrowBookAsync(BorrowTransactions borrow);  
        Task<BorrowTransactions?> GetBorrowTransaction(Guid id); 
        Task<bool> ReturnBookAsync(BorrowTransactions transaction); 
        Task<List<BorrowHistoryDto>> GetBorrowingHistoryByMemberId(Guid memberId);
    }
}
