using ArasvaAssignment.Application.Contracts.Identity;
using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Entities;
using ArasvaAssignment.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ArasvaAssignment.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILoggedInService _loggedInService;

        public MemberRepository(ApplicationDbContext applicationDbContext, ILoggedInService loggedInService)
        {
            _applicationDbContext = applicationDbContext;
            _loggedInService = loggedInService;
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            member.CreatedOn = DateTime.UtcNow;
            member.CreatedBy = _loggedInService.MemberId; 
            member.IsActive = true;

            await _applicationDbContext.Members.AddAsync(member);
            await _applicationDbContext.SaveChangesAsync();
            return member;
        }

        public async Task<IEnumerable<Member>> GetAllMemberAsync(string? search = null,bool? isActive = null)
        {
            var query = _applicationDbContext.Members.AsQueryable();

            if (isActive.HasValue)
                query = query.Where(m => m.IsActive == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                string keyword = search.Trim().ToLower();
                query = query.Where(m =>
                    m.Name.ToLower().Contains(keyword) ||
                    m.Email.ToLower().Contains(keyword) ||
                    m.Mobile.Contains(keyword)
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Member?> GetMemberById(Guid memberId)
        {
            return await _applicationDbContext.Members
                .FirstOrDefaultAsync(m => m.Id == memberId);
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            member.ModifiedOn = DateTime.UtcNow;
            member.ModifiedBy = _loggedInService.MemberId;  

            _applicationDbContext.Members.Update(member);
            await _applicationDbContext.SaveChangesAsync();
            return member;
        }

        public async Task<Member?> GetMemberByEmail(string email)
        {
            return await _applicationDbContext.Members
                .FirstOrDefaultAsync(m => m.Email == email && m.IsActive);
        }

        public async Task<bool> IsMemberExistsAsync(
            string email,
            string mobile,
            Guid? excludeMemberId = null)
        {
            return await _applicationDbContext.Members.AnyAsync(m =>
                (m.Email == email || m.Mobile == mobile) &&
                (excludeMemberId == null || m.Id != excludeMemberId)
            );
        }
    }
}
