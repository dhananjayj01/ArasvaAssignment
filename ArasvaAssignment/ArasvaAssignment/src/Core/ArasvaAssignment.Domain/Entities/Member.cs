using System.ComponentModel.DataAnnotations;

namespace ArasvaAssignment.Domain.Entities
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
    
        [Required]
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid ?CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }

        public bool IsActive { get; set; }=true;

        // RELATION → Member has Many BorrowTransactions
        public ICollection<BorrowTransactions> BorrowTransactions { get; set; } = new List<BorrowTransactions>();
        public ICollection<Review> Reviews { get; set; }
    }

}
