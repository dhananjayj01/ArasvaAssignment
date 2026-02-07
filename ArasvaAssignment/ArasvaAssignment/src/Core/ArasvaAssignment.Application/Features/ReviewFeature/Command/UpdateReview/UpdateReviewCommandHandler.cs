using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Command.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ApiResponse<ReviewDto>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ReviewDto>> Handle(
            UpdateReviewCommand request,  
            CancellationToken cancellationToken)
        {
            var review = await _repository.GetByIdAsync(request.ReviewId);

            if (review == null) {
                return new ApiResponse<ReviewDto>
                {
                    Success = false,
                    Message = "Review not found",
                    Data = null
                };
            }

            review.Rating = request.UpdateReviewDto.Rating;
            review.Comment = request.UpdateReviewDto.Comment;

            var updated = await _repository.UpdateAsync(review);

            if(!updated)
            {
                return new ApiResponse<ReviewDto>
                {
                    Success = false,
                    Message = "Review update failed",
                    Data = null
                };
            }

            return new ApiResponse<ReviewDto>
            {
                Success = true,
                Message = "Review updated successfully",
                Data = _mapper.Map<ReviewDto>(review)
            };
        }
    }
}
