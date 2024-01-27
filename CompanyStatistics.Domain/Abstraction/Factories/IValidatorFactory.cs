using FluentValidation;

namespace CompanyStatistics.Domain.Abstraction.Factories
{
    public interface IValidatorFactory
    {
        IValidator<T> GetValidator<T>();
    }
}
