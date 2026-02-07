namespace ArasvaAssignment.Application.Dtos.BorrowTransactionDtos
{
    public class BorrowBookDto
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
    }

    public class BorrowBookResponseData
    {
        public BorrowBookDto? BorrowedBook { get; set; }
        public DateTime DueDate { get; set; }
        public Guid? TransactionId { get; set; }    
    }
}
