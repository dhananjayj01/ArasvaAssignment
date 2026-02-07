using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.GetMemberById
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, ApiResponse<MemberDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<MemberDto>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetMemberById(request.MemberId);

            if (member == null || !member.IsActive)
            {
                return new ApiResponse<MemberDto>
                {
                    Success = false,
                    Message = "Member not found or inactive",
                    Data = null
                };
            }

            var memberDto = _mapper.Map<MemberDto>(member);

            return new ApiResponse<MemberDto>
            {
                Success = true,
                Message = "Member retrieved successfully",
                Data = memberDto
            };
        }
    }
}
