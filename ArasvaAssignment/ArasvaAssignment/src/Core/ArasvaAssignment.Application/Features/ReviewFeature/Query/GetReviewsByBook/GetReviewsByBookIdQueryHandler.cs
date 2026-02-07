using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Query.GetReviewsByBook
{
    public class GetReviewsByBookIdQueryHandler : IRequestHandler<GetReviewsByBookIdQuery, ApiResponse<List<ReviewDto>>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public GetReviewsByBookIdQueryHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ReviewDto>>> Handle(
            GetReviewsByBookIdQuery request,
            CancellationToken cancellationToken )
        {
            var reviews = await _repository.GetByBookIdAsync(request.BookId);

            if (reviews == null || !reviews.Any())
            {
                return new ApiResponse<List<ReviewDto>>
                {
                    Success = false,
                    Message = "No reviews found for this book",
                    Data = new List<ReviewDto>()
                };
            }

            return new ApiResponse<List<ReviewDto>>
            {
                Success = true,
                Message = "Reviews fetched successfully",
                Data = _mapper.Map<List<ReviewDto>>(reviews)
            };

        }
    }
}
