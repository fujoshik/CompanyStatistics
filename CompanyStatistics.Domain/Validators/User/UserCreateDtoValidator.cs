﻿using CompanyStatistics.Domain.DTOs.User;
using FluentValidation;

namespace CompanyStatistics.Domain.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Age).NotEmpty();

            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.AccountId).NotEmpty();
        }
    }
}
