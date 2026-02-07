using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Application.Features.BookCopyFeature.Command.AddBookCopy;
using ArasvaAssignment.Application.Features.BookCopyFeature.Command.UpdateBookCopy;
using ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetAllBookCopies;
using ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetBookCopyById;
using ArasvaAssignment.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopyController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BookCopyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddBookCopy")]
        public async Task<IActionResult> AddBookCopy([FromBody] AddBookCopyDto addBookCopyDto)
        {
            var result = await _mediator.Send(new AddBookCopyCommand(addBookCopyDto));
            return Ok(result);
        }

        [HttpGet("GetAllBookCopies")]
        public async Task<IActionResult> GetAllBookCopies([FromQuery] string? search, [FromQuery] bool? isAvailable)
        {
            var result = await _mediator.Send(new GetAllBookCopiesQuery(search, isAvailable));
            return Ok(result);
        }

        [HttpGet("GetBookCopyById")]
        public async Task<IActionResult> GetBookCopyById(string copyId)
        {
            if (!Guid.TryParse(copyId, out var validCopyId))
            {
                return BadRequest(new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Enter a valid Book Copy Id",
                    Data = null
                });
            }

            var result = await _mediator.Send(new GetBookCopyByIdQuery(validCopyId));
            return Ok(result);
        }

        [HttpPut("UpdateBookCopiesDetails")]
        public async Task<IActionResult> UpdateBookCopy([FromQuery] Guid CopyId, [FromBody] UpdateBookCopyDto updateBookCopyDto)
        {
            var result = await _mediator.Send(new UpdateBookCopyCommand(CopyId, updateBookCopyDto));
            return Ok(result);
        }
    }
}
