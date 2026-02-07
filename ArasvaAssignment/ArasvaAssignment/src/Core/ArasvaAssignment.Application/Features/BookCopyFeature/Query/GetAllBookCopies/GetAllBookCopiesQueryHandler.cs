using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetAllBookCopies
{
    public class GetAllBookCopiesQueryHandler : IRequestHandler<GetAllBookCopiesQuery, ApiResponse<IEnumerable<BookCopyDto>>>
    {
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IMapper _mapper;

        public GetAllBookCopiesQueryHandler(IBookCopyRepository bookCopyRepository, IMapper mapper)
        {
            _bookCopyRepository = bookCopyRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<BookCopyDto>>> Handle(GetAllBookCopiesQuery request, 
            CancellationToken cancellation)
        {
            var allBookCopies = await _bookCopyRepository.GetAllBookCopyAsync(request.Search, request.IsAvailable);

            var bookCopyDto = _mapper.Map<IEnumerable<BookCopyDto>>(allBookCopies);

            return new ApiResponse<IEnumerable<BookCopyDto>>
            {
                Success = true,
                Message = "Book copies retrieved successfully",
                Data = bookCopyDto
            };
        }
    }
}
