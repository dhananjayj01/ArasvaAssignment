using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Query.GetReviewsByBook
{
    public record GetReviewsByBookIdQuery(Guid BookId) : IRequest<ApiResponse<List<ReviewDto>>>;
}
