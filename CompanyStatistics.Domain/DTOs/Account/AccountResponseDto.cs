using CompanyStatistics.Domain.Enums;

namespace CompanyStatistics.Domain.DTOs.Account
{
    public class AccountResponseDto : BaseResponseDto
    {
        public string Email { get; set; }
        public Role Role { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
