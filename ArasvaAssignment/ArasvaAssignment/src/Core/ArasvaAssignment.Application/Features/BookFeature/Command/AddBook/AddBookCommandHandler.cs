using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;

namespace ArasvaAssignment.Application.Features.BookFeature.Command.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ApiResponse<BookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BookDto>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddBookDto;

            // Check for duplicate Title/ISBN
            var exists = await _bookRepository.IsBookExistsAsync(dto.Title, dto.ISBN);
            if (exists)
                return new ApiResponse<BookDto>
                {
                    Success = false,
                    Message = "Book with same Title or ISBN exists",
                    Data = null
                };

            var book = _mapper.Map<Book>(dto);
            await _bookRepository.AddBookAsync(book);

            return new ApiResponse<BookDto>
            {
                Success = true,
                Message = "Book added successfully",
                Data = _mapper.Map<BookDto>(book)
            };
        }
    }
}
