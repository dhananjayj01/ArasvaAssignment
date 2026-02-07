using ArasvaAssignment.Application.Dtos.MemberDtos;
using MediatR;

namespace ArasvaAssignment.Application.Features.MemberFeature.Query.LoginMember
{
    public class LoginMemberQuery : IRequest<LoginResponseDto>
    {
        public LoginRequestDto Login { get; set; }

        public LoginMemberQuery(LoginRequestDto login)
        {
            Login = login;
        }
    }
}
