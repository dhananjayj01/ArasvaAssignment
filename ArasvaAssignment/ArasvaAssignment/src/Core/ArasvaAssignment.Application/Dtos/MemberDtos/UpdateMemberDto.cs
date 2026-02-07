namespace ArasvaAssignment.Application.Dtos.MemberDtos
{
    public class UpdateMemberDto
    {          
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }    
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
