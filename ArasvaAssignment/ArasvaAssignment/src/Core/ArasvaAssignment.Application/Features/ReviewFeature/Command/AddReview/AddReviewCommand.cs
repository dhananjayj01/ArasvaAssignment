using ArasvaAssignment.Application.Dtos.ReviewDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Command.AddReview
{
    public record AddReviewCommand(AddReviewDto AddReviewDto) : IRequest<ApiResponse<ReviewDto>>;
}
