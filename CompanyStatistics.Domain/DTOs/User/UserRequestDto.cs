namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserRequestDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string AccountId { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
