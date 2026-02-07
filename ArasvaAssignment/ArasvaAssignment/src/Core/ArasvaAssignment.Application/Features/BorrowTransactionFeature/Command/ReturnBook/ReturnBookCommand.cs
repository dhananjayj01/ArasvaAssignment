using MediatR;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Domain.Common;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.ReturnBook
{
    public record ReturnBookCommand(ReturnBookDto ReturnDto) : IRequest<ApiResponse<ReturnBookDto>>;
}
