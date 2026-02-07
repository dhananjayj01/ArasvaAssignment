using ArasvaAssignment.Application.Contracts.Persistence;
using ArasvaAssignment.Application.Dtos.BorrowTransactionDtos;
using ArasvaAssignment.Domain.Common;
using ArasvaAssignment.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ArasvaAssignment.Application.Features.BorrowTransactionFeature.Command.BorrowBook
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, ApiResponse<BorrowBookResponseData>>
    {
        private readonly IBorrowTransactionsRepository _borrowRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BorrowBookCommandHandler(
            IBorrowTransactionsRepository borrowRepository,
            IMemberRepository memberRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _borrowRepository = borrowRepository;
            _memberRepository = memberRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BorrowBookResponseData>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var dto = request.BorrowDto;

            var book = await _bookRepository.GetBookById(dto.BookId);
            if (book == null || book.IsDeleted || !book.IsAvailable)
                return new ApiResponse<BorrowBookResponseData>
                {
                    Success = false,
                    Message = book == null ? "Book not found" :
                              book.IsDeleted ? "Book has been removed" :
                              "Book is already borrowed",
                    Data = null
                };

            var member = await _memberRepository.GetMemberById(dto.MemberId);
            if (member == null)
                return new ApiResponse<BorrowBookResponseData>
                {
                    Success = false,
                    Message = "Member not found",
                    Data = null
                };

            var borrowEntity = _mapper.Map<BorrowTransactions>(dto);
            var transaction = await _borrowRepository.BorrowBookAsync(borrowEntity);

            var responseData = new BorrowBookResponseData
            {
                BorrowedBook = dto,
                TransactionId = transaction.Id,
                DueDate = transaction.DueDate
            };

            return new ApiResponse<BorrowBookResponseData>
            {
                Success = true,
                Message = "Book borrowed successfully",
                Data = responseData
            };
        }
    }
}
