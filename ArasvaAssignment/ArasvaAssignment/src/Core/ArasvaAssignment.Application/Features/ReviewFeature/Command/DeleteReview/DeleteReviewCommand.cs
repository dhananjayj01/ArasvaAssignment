using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.ReviewFeature.Command.DeleteReview
{
    public record DeleteReviewCommand(Guid ReviewId) : IRequest<ApiResponse<bool>>;
}
