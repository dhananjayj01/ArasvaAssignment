using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.GetAllMember
{
    public record GetAllMemberQuery(string? search = null, bool? isActive = null)
        : IRequest<ApiResponse<IEnumerable<MemberDto>>>;
}
