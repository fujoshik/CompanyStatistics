namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserCreateDto : UserCreateWithoutAccountIdDto
    {
        public string AccountId { get; set; }
    }
}
