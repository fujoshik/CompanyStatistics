namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserCreateWithoutAccountIdDto : UserWithoutIdDto
    {
        public int IsDeleted { get; set; } = 0;
    }
}
