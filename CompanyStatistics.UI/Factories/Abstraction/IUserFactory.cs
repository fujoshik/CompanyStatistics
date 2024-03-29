﻿using CompanyStatistics.Domain.DTOs.User;

namespace CompanyStatistics.UI.Factories.Abstraction
{
    public interface IUserFactory
    {
        UserCreateWithoutAccountIdDto CreateUserCreateDto(string firstName, string lastName,
            string country, int age);
        UserRequestDto CreateUserRequestDto(string firstName, string lastName,
            string country, int age, string accountId);
    }
}
