using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.GetMemberById
{
    public record GetMemberByIdQuery(Guid MemberId) : IRequest<ApiResponse<MemberDto>>;
}
