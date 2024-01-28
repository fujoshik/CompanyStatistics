namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
