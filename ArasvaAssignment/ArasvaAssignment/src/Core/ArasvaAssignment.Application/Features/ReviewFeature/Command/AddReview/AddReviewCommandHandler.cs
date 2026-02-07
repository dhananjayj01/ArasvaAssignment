using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Command.AddReview
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, ApiResponse<ReviewDto>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public AddReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ReviewDto>> Handle(
            AddReviewCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AddReviewDto;

            // Map DTO → Entity
            var review = _mapper.Map<Review>(dto);

            await _repository.AddAsync(review);

            return new ApiResponse<ReviewDto>
            {
                Success = true,
                Message = "Review added successfully",
                Data = _mapper.Map<ReviewDto>(review)
            };
        }
    }
}
