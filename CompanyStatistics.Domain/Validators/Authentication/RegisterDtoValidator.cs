using CompanyStatistics.Domain.DTOs.Authentication;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompanyStatistics.Domain.Validators.Authentication
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*+_-]).{8,}$");

        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Age).NotEmpty()
                               .GreaterThanOrEqualTo(18);

            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty()
                                    .Matches(passwordRegex)
                                    .WithMessage("Password must contain at leas 8 characters, a capital letter, a lowercase letter, a number and a special symbol");
        }
    }
}
