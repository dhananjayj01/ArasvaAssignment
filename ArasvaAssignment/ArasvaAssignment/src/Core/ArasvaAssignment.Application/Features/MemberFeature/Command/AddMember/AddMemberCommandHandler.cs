using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.MemberFeature.Command.AddMember
{
    public class AddMemberCommandHandler
        : IRequestHandler<AddMemberCommand, ApiResponse<MemberDto>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<Member> _passwordHasher = new();

        public AddMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<MemberDto>> Handle(
            AddMemberCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AddMemberDto;

            // Uniqueness check
            var exists = await _memberRepository.IsMemberExistsAsync(dto.Email, dto.Mobile);
            if (exists)
            {
                return new ApiResponse<MemberDto>
                {
                    Success = false,
                    Message = "Email or Mobile number already exists"
                };
            }

            // Map DTO → Entity
            var member = _mapper.Map<Member>(dto);

            // ✅ HASH PASSWORD (NOT ENCRYPT)
            member.Password = _passwordHasher.HashPassword(member, dto.Password);

            await _memberRepository.AddMemberAsync(member);

            return new ApiResponse<MemberDto>
            {
                Success = true,
                Message = "Member registered successfully",
                Data = _mapper.Map<MemberDto>(member)
            };
        }
    }
}
