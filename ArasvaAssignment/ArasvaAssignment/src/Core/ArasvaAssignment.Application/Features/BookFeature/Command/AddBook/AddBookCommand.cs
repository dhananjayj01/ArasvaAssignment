using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookFeature.Command.AddBook
{
    public record AddBookCommand(AddBookDto AddBookDto) : IRequest<ApiResponse<BookDto>>;
}
