namespace ArasvaAssignment.Application.Dtos.MemberDtos
{
    public class LoginRequestDto
    {
        public string Username { get; set; }    // email or mobile
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public MemberDto? Member { get; set; }
        public string Token { get; set; }
    }
}
