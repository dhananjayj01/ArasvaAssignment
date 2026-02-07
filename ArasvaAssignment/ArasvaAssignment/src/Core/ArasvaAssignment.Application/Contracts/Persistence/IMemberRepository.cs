using ArasvaAssignment.Domain.Entities;

namespace ArasvaAssignment.Application.Contracts.Persistence
{
    public interface IMemberRepository
    {
        Task<Member> AddMemberAsync(Member member); 
        Task<IEnumerable<Member>> GetAllMemberAsync(string? search = null, bool? isActive = null); 
        Task<Member?> GetMemberById(Guid memberId); 
        Task<Member> UpdateMemberAsync(Member member); 
        Task<Member?> GetMemberByEmail(string email); 
        Task<bool> IsMemberExistsAsync(string email, string mobile,Guid? excludeMemberId = null );

    }
}
