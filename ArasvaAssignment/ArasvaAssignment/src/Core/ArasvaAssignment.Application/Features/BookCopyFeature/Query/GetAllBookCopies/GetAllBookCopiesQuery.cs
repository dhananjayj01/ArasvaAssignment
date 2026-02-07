using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetAllBookCopies
{
    public record GetAllBookCopiesQuery : IRequest<ApiResponse<IEnumerable<BookCopyDto>>>
    {
        public string? Search { get; set; }
        public bool? IsAvailable { get; set; }

        public GetAllBookCopiesQuery(string? search = null, bool? isAvailable = null)
        {
            Search = search;
            IsAvailable = isAvailable;
        }
    }
}
