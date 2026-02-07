using System.ComponentModel.DataAnnotations;

namespace ArasvaAssignment.Domain.Entities
{
    public class Book
    {
        [Key]    
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }
        [Required]
        public int ISBN { get; set; }
        public string? Category { get; set; }
        [Required]
        public string Author { get; set; }
        public string? Publisher { get; set; }
        public bool IsAvailable { get; set; }= true;

        public DateTime CreatedOn { get; set; }
        public Guid ?CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        // RELATION → Book has Many BorrowTransactions
        public ICollection<BorrowTransactions> BorrowTransactions { get; set; } = new List<BorrowTransactions>();
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookCopy> BookCopys { get; set; }
    }
}
