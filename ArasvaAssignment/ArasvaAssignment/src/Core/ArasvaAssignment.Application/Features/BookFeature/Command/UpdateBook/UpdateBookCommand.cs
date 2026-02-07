using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookFeature.Command.UpdateBook
{
    public record UpdateBookCommand(Guid BookId, UpdateBookDetailsDto UpdateBookDetailsDto)
        : IRequest<ApiResponse<BookDto>>;
}
