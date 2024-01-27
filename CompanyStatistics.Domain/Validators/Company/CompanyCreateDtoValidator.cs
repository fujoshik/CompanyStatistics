using CompanyStatistics.Domain.DTOs.Company;
using FluentValidation;

namespace CompanyStatistics.Domain.Validators.Company
{
    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateDtoValidator()
        {
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
