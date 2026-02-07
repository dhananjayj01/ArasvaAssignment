using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Application.Features.MemberFeature.Query.LoginMember;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var response = await _mediator.Send(new LoginMemberQuery(dto));

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
