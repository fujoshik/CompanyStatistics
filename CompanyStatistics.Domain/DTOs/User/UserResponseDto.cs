namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserResponseDto : BaseResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string AccountId { get; set; }
    }
}
