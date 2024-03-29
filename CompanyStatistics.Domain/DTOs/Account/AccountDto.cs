﻿using CompanyStatistics.Domain.Enums;

namespace CompanyStatistics.Domain.DTOs.Account
{
    public class AccountDto : BaseResponseDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
