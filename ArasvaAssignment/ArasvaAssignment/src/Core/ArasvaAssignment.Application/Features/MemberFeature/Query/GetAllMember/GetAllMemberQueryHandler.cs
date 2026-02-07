using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.GetAllMember
{
    public class GetAllMemberQueryHandler : IRequestHandler<GetAllMemberQuery, ApiResponse<IEnumerable<MemberDto>>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetAllMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<MemberDto>>> Handle(GetAllMemberQuery request, CancellationToken cancellationToken)
        {
            var allMembers = await _memberRepository.GetAllMemberAsync(request.search, request.isActive);

            if (allMembers == null || !allMembers.Any())
            {
                return new ApiResponse<IEnumerable<MemberDto>>
                {
                    Success = false,
                    Message = "No members found",
                    Data = null
                };
            }

            var memberDtos = _mapper.Map<IEnumerable<MemberDto>>(allMembers);

            return new ApiResponse<IEnumerable<MemberDto>>
            {
                Success = true,
                Message = "Members retrieved successfully",
                Data = memberDtos
            };
        }
    }
}
