using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Application.Dtos.BookDtos;
using MediatR;
using System;

namespace ArasvaAssignment.Application.Features.BookFeature.Query.GetBookById
{
    public record GetBookByIdQuery(Guid BookId) : IRequest<ApiResponse<BookDto>>; 
}
