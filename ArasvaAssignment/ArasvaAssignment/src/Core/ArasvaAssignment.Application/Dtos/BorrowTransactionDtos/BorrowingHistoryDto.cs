namespace ArasvaAssignment.Application.Dtos.BorrowTransactionDtos
{
    public class BorrowingHistoryDto
    {
        public class BorrowHistoryDto
        {
            public Guid TransactionId { get; set; }
            public string BookTitle { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public bool IsReturned { get; set; }    
        }
    }
}
