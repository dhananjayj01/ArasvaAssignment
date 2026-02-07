using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.MemberFeature.Command.UpdateMember
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, ApiResponse<MemberDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<MemberDto>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateMemberDto;

            // Check if member exists in DB
            var existingMember = await _memberRepository.GetMemberById(request.MemberId);
            if (existingMember == null)
            {
                return new ApiResponse<MemberDto>
                {
                    Success = false,
                    Message = "Member not found",
                    Data = null
                };
            }

            // Check uniqueness of Email and Mobile excluding this member
            var exists = await _memberRepository.IsMemberExistsAsync(dto.Email, dto.Mobile, request.MemberId);
            if (exists)
            {
                return new ApiResponse<MemberDto>
                {
                    Success = false,
                    Message = "Email or Mobile number already exists",
                    Data = null
                };
            }

            // Map updated fields from DTO → Entity
            _mapper.Map(dto, existingMember);

            // Save changes
            await _memberRepository.UpdateMemberAsync(existingMember);

            return new ApiResponse<MemberDto>
            {
                Success = true,
                Message = "Member updated successfully",
                Data = _mapper.Map<MemberDto>(existingMember)
            };
        }
    }
}
