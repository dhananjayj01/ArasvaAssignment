using ArasvaAssignment.Domain.Entities;

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(string? search = null, bool? isActive = null);
        Task<Category?> GetByIdAsync(int categoryId);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<bool> IsCategoryExistsAsync(string Name, int? excludeCategoryId);
    }
}
