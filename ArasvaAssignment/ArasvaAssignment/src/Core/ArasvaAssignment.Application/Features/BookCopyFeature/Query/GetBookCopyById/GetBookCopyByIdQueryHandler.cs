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

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Query.GetBookCopyById
{
    public class GetBookCopyByIdQueryHandler : IRequestHandler<GetBookCopyByIdQuery, ApiResponse<BookCopyDto>>
    {
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IMapper _mapper;

        public GetBookCopyByIdQueryHandler(
            IBookCopyRepository repository,
            IMapper mapper )
        {
            _bookCopyRepository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BookCopyDto>> Handle(
            GetBookCopyByIdQuery request,
            CancellationToken cancellationToken)
        {
            var bookCopy = await _bookCopyRepository.GetBookCopyById(request.CopyId);

            if (bookCopy == null || bookCopy.IsDeleted)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Book copy not found or is deleted",
                    Data = null
                };
            }

            var bookCopyDto = _mapper.Map<BookCopyDto>(bookCopy);

            return new ApiResponse<BookCopyDto>
            {
                Success = true,
                Message = "Book copy retrieved successfully",
                Data = bookCopyDto
            };
        }
    }
}
