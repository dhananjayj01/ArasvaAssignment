using ArasvaAssignment.Application.Dtos.CategoryDtos;
using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Application.Features.ReviewFeature.Command.AddReview;
using ArasvaAssignment.Application.Features.ReviewFeature.Command.DeleteReview;
using ArasvaAssignment.Application.Features.ReviewFeature.Command.UpdateReview;
using ArasvaAssignment.Application.Features.ReviewFeature.Query.GetReviewsByBook;
using ArasvaAssignment.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArasvaAssignment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Add Review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDto dto)
        {
            var result = await _mediator.Send(new AddReviewCommand(dto));
            return Ok(result);
        }

        // Get Reviews by Book
        [HttpGet("GetReviewsByBookId")]
        public async Task<IActionResult> GetReviewsByBook([FromQuery] string bookId)
        {
            if (!Guid.TryParse(bookId, out Guid validBookId))
            {
                return BadRequest(new ApiResponse<ReviewDto>
                {
                    Success = false,
                    Message = "Enter a valid Book Id",
                    Data = null
                });
            }
            var result = await _mediator.Send(
                new GetReviewsByBookIdQuery(validBookId));
            return Ok(result);
        }

        // Delete Review
        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> Delete(Guid reviewId)
        {
            var result = await _mediator.Send(
                new DeleteReviewCommand(reviewId));
            return Ok(result);
        }

        // Update Review
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> Update(Guid reviewId, [FromBody] UpdateReviewDto dto)
        {
            var result = await _mediator.Send(
                new UpdateReviewCommand(reviewId, dto));
            return Ok(result);
        }
    }
}
