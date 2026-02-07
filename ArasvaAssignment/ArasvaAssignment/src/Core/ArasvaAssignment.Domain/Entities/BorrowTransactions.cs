using System.ComponentModel.DataAnnotations;

namespace ArasvaAssignment.Domain.Entities
{
    public class BorrowTransactions
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }

        
        public Book Book { get; set; }
        public Member Member { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid ?CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
