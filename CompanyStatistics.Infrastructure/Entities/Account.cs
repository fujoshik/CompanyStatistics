using CompanyStatistics.Domain.Enums;

namespace CompanyStatistics.Infrastructure.Entities
{
    public class Account : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public int IsDeleted { get; set; }
    }
}