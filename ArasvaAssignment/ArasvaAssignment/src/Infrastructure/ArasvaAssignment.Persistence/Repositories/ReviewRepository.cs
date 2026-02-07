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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetByIdAsync(Guid reviewId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId && r.IsActive);
        }

        public async Task<List<Review>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId && r.IsActive)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid reviewId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReviewId == reviewId);

            if (review == null)
                return false;

            if (!review.IsActive)
                return false; // already deleted

            review.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Review review)
        {
            var existingReview = await _context.Reviews.IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.ReviewId == review.ReviewId);

            if(existingReview == null || !existingReview.IsActive)
                return false;

            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
