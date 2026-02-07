using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;

namespace ArasvaAssignment.Application.Features.MemberFeature.Command.UpdateMember
{
    public record UpdateMemberCommand(Guid MemberId, UpdateMemberDto UpdateMemberDto)
        : IRequest<ApiResponse<MemberDto>>;
}
