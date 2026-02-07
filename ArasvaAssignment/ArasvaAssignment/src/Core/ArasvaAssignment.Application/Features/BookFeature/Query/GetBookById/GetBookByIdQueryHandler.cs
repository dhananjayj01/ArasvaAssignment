using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BookFeature.Query.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ApiResponse<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookById(request.BookId);

            if (book == null || book.IsDeleted)
            {
                return new ApiResponse<BookDto>
                {
                    Success = false,
                    Message = "Book not found or is deleted",
                    Data = null
                };
            }

            var bookDto = _mapper.Map<BookDto>(book);

            return new ApiResponse<BookDto>
            {
                Success = true,
                Message = "Book retrieved successfully",
                Data = bookDto
            };
        }
    }
}
