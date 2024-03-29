﻿namespace CompanyStatistics.Domain.DTOs.Account
{
    public class AccountRequestDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int Role { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
