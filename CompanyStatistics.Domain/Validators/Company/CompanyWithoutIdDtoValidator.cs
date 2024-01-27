using CompanyStatistics.Domain.DTOs.Company;
using FluentValidation;

namespace CompanyStatistics.Domain.Validators.Company
{
    public class CompanyWithoutIdDtoValidator : AbstractValidator<CompanyWithoutIdDto>
    {
        public CompanyWithoutIdDtoValidator()
        {
            RuleFor(x => x.CompanyIndex).NotEmpty();

            RuleFor(x => x.Name).NotEmpty();

            RuleFor(x => x.NumberOfEmployees).NotEmpty();

            RuleFor(x => x.Country).NotEmpty();

            RuleFor(x => x.Description).NotEmpty();

            RuleFor(x => x.Founded).NotEmpty();

            RuleFor(x => x.Website).NotEmpty();

            RuleFor(x => x.Industries).NotEmpty();
        }
    }
}
