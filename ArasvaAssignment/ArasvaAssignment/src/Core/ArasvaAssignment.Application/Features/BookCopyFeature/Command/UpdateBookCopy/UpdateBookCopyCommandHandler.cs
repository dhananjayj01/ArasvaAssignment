using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BookCopyDtos;
using ArasvaAssignment.Application.Dtos.BookDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BookCopyFeature.Command.UpdateBookCopy
{
    public class UpdateBookCopyCommandHandler : IRequestHandler<UpdateBookCopyCommand, ApiResponse<BookCopyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBookCopyRepository _bookCopyRepository;

        public UpdateBookCopyCommandHandler(IMapper mapper, IBookCopyRepository bookCopyRepository)
        {
            _mapper = mapper;
            _bookCopyRepository = bookCopyRepository;
        }

        public async Task<ApiResponse<BookCopyDto>> Handle(UpdateBookCopyCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.UpdateBookCopyDto;

            var existingBookCopy = await _bookCopyRepository.GetBookCopyById(request.CopyId);

            if (existingBookCopy == null || existingBookCopy.IsDeleted)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Book copy not found or deleted",
                    Data = null
                };
            }

            // Check duplicate Barcode
            var duplicateExists = await _bookCopyRepository.IsBookCopyExistsAsync(
                dto.Barcode,
                request.CopyId);

            if (duplicateExists)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Another book copy with same Barcode already exists",
                    Data = null
                };
            }

            // Validate status transition (important)
            if (existingBookCopy.Status == BookCopyStatus.Lost)
            {
                return new ApiResponse<BookCopyDto>
                {
                    Success = false,
                    Message = "Lost book copy cannot be modified",
                    Data = null
                };
            }

            // Map allowed fields
            existingBookCopy.Barcode = dto.Barcode;
            existingBookCopy.Status = dto.Status;
            existingBookCopy.ModifiedOn = DateTime.UtcNow;

            // Map DTO → Entity
            _mapper.Map(dto, existingBookCopy);

            await _bookCopyRepository.UpdateBookCopyAsync(existingBookCopy);

            var bookCopyDto = _mapper.Map<BookCopyDto>(existingBookCopy);

            return new ApiResponse<BookCopyDto>
            {
                Success = true,
                Message = "Book copy details updated successfully",
                Data = bookCopyDto
            };

        }
    }
}
