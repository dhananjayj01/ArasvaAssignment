using MediatR;
using ArasvaAssignment.Domain.Common;
using static ArasvaAssignment.Application.Dtos.BorrowTransactionDtos.BorrowingHistoryDto;
using System;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Query.GetBorrowingHistory
{
    public class GetBorrowingHistoryQuery : IRequest<ApiResponse<List<BorrowHistoryDto>>>
    {
        public Guid MemberId { get; set; }
        public GetBorrowingHistoryQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }
}
