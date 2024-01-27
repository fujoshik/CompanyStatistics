using CompanyStatistics.Domain.DTOs.Account;
using FluentValidation;

namespace CompanyStatistics.Domain.Validators.Account
{
    public class AccountRequestDtoValidator : AbstractValidator<AccountRequestDto>
    {
        public AccountRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Role).NotEmpty();

            RuleFor(x => x.PasswordHash).NotEmpty();

            RuleFor(x => x.PasswordSalt).NotEmpty();
        }
    }
}
