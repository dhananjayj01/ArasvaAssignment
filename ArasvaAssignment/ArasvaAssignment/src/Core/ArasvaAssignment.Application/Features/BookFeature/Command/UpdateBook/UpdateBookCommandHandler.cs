using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookFeature.Command.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ApiResponse<BookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateBookDetailsDto;

            var existingBook = await _bookRepository.GetBookById(request.BookId);

            if (existingBook == null || existingBook.IsDeleted)
            {
                return new ApiResponse<BookDto>
                {
                    Success = false,
                    Message = "Book not found or deleted",
                    Data = null
                };
            }

            // Check duplicate Title/ISBN
            var duplicateExists = await _bookRepository.IsBookExistsAsync(
                dto.Title,
                dto.ISBN,
                request.BookId);

            if (duplicateExists)
            {
                return new ApiResponse<BookDto>
                {
                    Success = false,
                    Message = "Another book with same Title or ISBN already exists",
                    Data = null
                };
            }

            // Map DTO → Entity
            _mapper.Map(dto, existingBook);

            await _bookRepository.UpdateBookAsync(existingBook);

            var bookDto = _mapper.Map<BookDto>(existingBook);

            return new ApiResponse<BookDto>
            {
                Success = true,
                Message = "Book updated successfully",
                Data = bookDto
            };
        }
    }
}
