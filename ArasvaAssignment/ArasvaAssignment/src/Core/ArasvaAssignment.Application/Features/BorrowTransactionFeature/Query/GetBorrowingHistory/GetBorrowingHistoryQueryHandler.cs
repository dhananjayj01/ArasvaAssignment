using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Domain.Common;
using MediatR;
using static ArasvaAssignment.Application.Dtos.BorrowTransactionDtos.BorrowingHistoryDto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Query.GetBorrowingHistory
{
    public class GetBorrowingHistoryQueryHandler : IRequestHandler<GetBorrowingHistoryQuery, ApiResponse<List<BorrowHistoryDto>>>
    {
        private readonly IBorrowTransactionsRepository _borrowRepository;

        public GetBorrowingHistoryQueryHandler(IBorrowTransactionsRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        public async Task<ApiResponse<List<BorrowHistoryDto>>> Handle(GetBorrowingHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _borrowRepository.GetBorrowingHistoryByMemberId(request.MemberId);

            if (history == null || history.Count == 0)
            {
                return new ApiResponse<List<BorrowHistoryDto>>
                {
                    Success = false,
                    Message = "No borrowing history found",
                    Data = null
                };
            }

            return new ApiResponse<List<BorrowHistoryDto>>
            {
                Success = true,
                Message = "Borrowing history retrieved successfully",
                Data = history
            };
        }
    }
}
