using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Domain.Entities;
using ArasvaAssignment.Infrastructure.Helper;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.LoginMember
{
    public class LoginMemberQueryHandler
        : IRequestHandler<LoginMemberQuery, LoginResponseDto>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        // ✅ PasswordHasher
        private readonly PasswordHasher<Member> _passwordHasher = new();

        public LoginMemberQueryHandler(
            IMemberRepository memberRepository,
            IMapper mapper,
            JwtHelper jwtHelper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseDto> Handle(
            LoginMemberQuery request,
            CancellationToken cancellationToken)
        {
            var login = request.Login;

            // USERNAME = Email
            var user = await _memberRepository.GetMemberByEmail(login.Username);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username"
                };
            }

            if (!user.IsActive)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Account is disabled"
                };
            }

            // ✅ VERIFY HASHED PASSWORD
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                login.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }

            // ✅ GENERATE JWT
            var token = _jwtHelper.GenerateToken(user.Id, user.Email);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                Member = _mapper.Map<MemberDto>(user),
                Token = token
            };
        }
    }
}
