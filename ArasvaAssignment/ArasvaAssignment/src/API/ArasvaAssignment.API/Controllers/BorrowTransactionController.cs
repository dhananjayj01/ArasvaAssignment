using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.BorrowBook;
using ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.ReturnBook;
using ArasvaAssignment.Application.Features.BorrowTransactionFeature.Query.GetBorrowingHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArasvaAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BorrowTransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("BorrowBook")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto borrowBookDto)
        {
            var result = await _mediator.Send(new BorrowBookCommand(borrowBookDto));
            return Ok(result);
        }

        [HttpPost("ReturnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            var result = await _mediator.Send(new ReturnBookCommand(returnBookDto));
            return Ok(result);
        }

        [HttpGet("BorrowingHistoryByMember")]
        public async Task<IActionResult> GetBorrowingHistory([FromQuery] Guid memberId)
        {
            var result = await _mediator.Send(new GetBorrowingHistoryQuery(memberId));
            return Ok(result);
        }
    }
}
