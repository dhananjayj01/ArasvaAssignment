using ArasvaAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<Review> GetByIdAsync(Guid reviewId);

        Task<List<Review>> GetByBookIdAsync(Guid bookId);
        Task<bool> DeleteAsync(Guid reviewId);

        Task<bool> UpdateAsync(Review review);
    }
}
