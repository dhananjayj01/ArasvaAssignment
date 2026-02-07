using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Command.AddBookCopy
{
    public class AddBookCopyCommandHandler : IRequestHandler<AddBookCopyCommand, ApiResponse<BookCopyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IBookRepository _bookRepository;

        public AddBookCopyCommandHandler(IMapper mapper, IBookCopyRepository bookCopyRepository,
            IBookRepository bookRepository)
        {
            _mapper = mapper;
            _bookCopyRepository = bookCopyRepository;
            _bookRepository = bookRepository;
        }

        public async Task<ApiResponse<BookCopyDto>> Handle(AddBookCopyCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.AddBookCopyDto;

            // Verify Book Exists
            var book = await _bookRepository.GetBookById(dto.BookId);
            if (book == null)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Book does not exist",
                    Data = null
                };
            }

            // Check duplicate barcode
            var copyExists = await _bookCopyRepository.IsBookCopyExistsAsync(dto.Barcode);
            if (copyExists)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Book copy with the same barcode is already exists",
                    Data = null
                };
            }

            var bookCopy = _mapper.Map<BookCopy>(dto);

            // Set default lifecycle state
            bookCopy.Status = BookCopyStatus.Available;
            bookCopy.CreatedOn = DateTime.UtcNow;

            await _bookCopyRepository.AddBookCopyAsync(bookCopy);

            return new ApiResponse<BookCopyDto>
            {
                Success = true,
                Message = "Book copy added successfully",
                Data = _mapper.Map<BookCopyDto>(bookCopy)
            };
        }
    }
}
