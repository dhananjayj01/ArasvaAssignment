using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Command.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ApiResponse<bool>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(
            DeleteReviewCommand request,
            CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(request.ReviewId);

            if (!deleted)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Review not found or already deleted",
                    Data = false
                };
            }

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Review deleted successfully",
                Data = true
            };
        }
    }
}
