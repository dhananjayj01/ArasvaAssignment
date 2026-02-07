using System.ComponentModel.DataAnnotations;

namespace ArasvaAssignment.Application.Dtos.MemberDtos
{
    public class AddMemberDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }   // match entity property name

        [Required]
        [RegularExpression(
           "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
           ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character."
       )]
        public string Password { get; set; }
    }
}
