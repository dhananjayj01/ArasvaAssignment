using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Command.UpdateBookCopy
{
    public record UpdateBookCopyCommand(Guid CopyId, UpdateBookCopyDto UpdateBookCopyDto)
        : IRequest<ApiResponse<BookCopyDto>>;
}
