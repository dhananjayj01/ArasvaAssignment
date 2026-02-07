using System;
using System.Collections.Generic;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookFeature.Query.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<ApiResponse<IEnumerable<BookDto>>>
    {
        public string? Search { get; set; }
        public bool? IsAvailable { get; set; }  

        public GetAllBooksQuery(string? search = null, bool? isAvailable = null)
        {
            Search = search;
            IsAvailable = isAvailable;
        }
    }
}
