using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Application.Features.BookFeature.Command.AddBook;
using ArasvaAssignment.Application.Features.BookFeature.Command.UpdateBook;
using ArasvaAssignment.Application.Features.BookFeature.Query.GetAllBooks;
using ArasvaAssignment.Application.Features.BookFeature.Query.GetBookById;
using ArasvaAssignment.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            var result = await _mediator.Send(new AddBookCommand(addBookDto));
            return Ok(result);
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? search, [FromQuery] bool? isAvailable)
        {
            var result = await _mediator.Send(new GetAllBooksQuery(search, isAvailable));
            return Ok(result);
        }

        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid validBookId))
            {
                return BadRequest(new ApiResponse<BookDto>
                {
                    Success = false,
                    Message = "Enter a valid Book Id",
                    Data = null
                });
            }

            var result = await _mediator.Send(new GetBookByIdQuery(validBookId));
            return Ok(result);
        }


        [HttpPut("UpdateBookDetails")]
        public async Task<IActionResult> UpdateBookDetails([FromQuery] Guid bookId, [FromBody] UpdateBookDetailsDto updateBookDetailsDto)
        {
            var result = await _mediator.Send(new UpdateBookCommand(bookId, updateBookDetailsDto));
            return Ok(result);
        }
    }
}
