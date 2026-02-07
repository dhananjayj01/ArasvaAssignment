using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.MemberFeature.Command.AddMember
{
    public record AddMemberCommand(AddMemberDto AddMemberDto) : IRequest<ApiResponse<MemberDto>>;
}
