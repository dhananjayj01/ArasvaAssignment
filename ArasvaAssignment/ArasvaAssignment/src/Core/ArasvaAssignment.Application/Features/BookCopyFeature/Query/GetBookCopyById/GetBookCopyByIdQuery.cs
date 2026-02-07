using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetBookCopyById
{
    public record GetBookCopyByIdQuery(Guid CopyId): IRequest<ApiResponse<BookCopyDto>>;
}
