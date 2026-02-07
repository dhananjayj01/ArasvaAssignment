using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Command.AddBookCopy
{
    public record AddBookCopyCommand(AddBookCopyDto AddBookCopyDto) : IRequest<ApiResponse<BookCopyDto>>;
}
