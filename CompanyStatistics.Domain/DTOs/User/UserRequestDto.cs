namespace CompanyStatistics.Domain.DTOs.User
{
    public class UserRequestDto : UserCreateDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
