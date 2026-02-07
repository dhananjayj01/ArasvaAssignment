using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Application.Dtos.MemberDtos;
using ArasvaAssignment.Application.Features.MemberFeature.Command.AddMember;
using ArasvaAssignment.Application.Features.MemberFeature.Command.UpdateMember;
using ArasvaAssignment.Application.Features.MemberFeature.Query.GetAllMember;
using ArasvaAssignment.Application.Features.MemberFeature.Query.GetMemberById;
using ArasvaAssignment.Application.Features.MemberFeature.Query.LoginMember;
using ArasvaAssignment.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MemberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddMember")]
        public async Task<IActionResult> AddMember([FromBody] AddMemberDto addMemberDto)
        {
            var result = await _mediator.Send(new AddMemberCommand(addMemberDto));
            return Ok(result);
        }

        [HttpGet("GetAllMember")]
        public async Task<IActionResult> GetAllMember([FromQuery] string? search = null, [FromQuery] bool? isActive = null)
        {
            var result = await _mediator.Send(new GetAllMemberQuery(search, isActive));
            return Ok(result);
        }


        [HttpGet("GetMemberById")]
        public async Task<IActionResult> GetMemberById([FromQuery] string memberId)
        {
            if (!Guid.TryParse(memberId, out Guid validMemberId))
            {
                return BadRequest(new ApiResponse<MemberDto>
                {
                    Success = false,
                    Message = "Enter a valid Member Id",
                    Data = null
                });
            }

            var result = await _mediator.Send(new GetMemberByIdQuery(validMemberId));
            return Ok(result);
        }


        [HttpPut("UpdateMemberDetails")]
        public async Task<IActionResult> UpdateMember([FromQuery] Guid memberId, [FromBody] UpdateMemberDto updateMemberDto)
        {
            var result = await _mediator.Send(new UpdateMemberCommand(memberId, updateMemberDto));
            return Ok(result);
        } 
    }
}
