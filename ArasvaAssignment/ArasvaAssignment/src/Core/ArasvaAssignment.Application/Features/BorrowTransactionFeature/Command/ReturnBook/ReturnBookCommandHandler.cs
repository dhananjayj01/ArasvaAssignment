using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Domain.Common;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, ApiResponse<ReturnBookDto>>
    {
        private readonly IBorrowTransactionsRepository _borrowRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ReturnBookCommandHandler(
            IBorrowTransactionsRepository borrowRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ReturnBookDto>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ReturnDto;

            var trx = await _borrowRepository.GetBorrowTransaction(dto.Id);
            if (trx == null)
                return new ApiResponse<ReturnBookDto> { Success = false, Message = "Transaction not found", Data = null };

            if (trx.ReturnDate != null)
                return new ApiResponse<ReturnBookDto> { Success = false, Message = "Book already returned", Data = null };

            trx.ReturnDate = dto.ReturnDate ?? DateTime.UtcNow;

            await _borrowRepository.ReturnBookAsync(trx);

            return new ApiResponse<ReturnBookDto>
            {
                Success = true,
                Message = "Book returned successfully",
                Data = dto
            };
        }
    }
}
