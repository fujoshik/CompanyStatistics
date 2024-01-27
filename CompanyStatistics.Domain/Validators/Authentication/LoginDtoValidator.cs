using CompanyStatistics.Domain.DTOs.Authentication;
using FluentValidation;

namespace CompanyStatistics.Domain.Validators.Authentication
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
