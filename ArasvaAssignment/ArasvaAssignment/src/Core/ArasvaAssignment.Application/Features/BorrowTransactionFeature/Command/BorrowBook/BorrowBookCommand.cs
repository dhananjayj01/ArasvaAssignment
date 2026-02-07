using MediatR;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Domain.Common;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.BorrowBook
{
    public record BorrowBookCommand(BorrowBookDto BorrowDto) : IRequest<ApiResponse<BorrowBookResponseData>>;
}
