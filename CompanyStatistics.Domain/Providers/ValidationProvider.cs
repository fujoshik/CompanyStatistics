using CompanyStatistics.Domain.Abstraction.Providers;
using CompanyStatistics.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = CompanyStatistics.Domain.Exceptions.ValidationException;

namespace CompanyStatistics.Domain.Providers
{
    public class ValidationProvider : IValidationProvider
    {
        private IServiceProvider _serviceProvider;

        public ValidationProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void TryValidate<TDto>(TDto dto)
        {
            var result = TryGetValidator<TDto>().Validate(dto);

            ThrowIfInvalid(result);
        }

        public async Task TryValidateAsync<TDto>(TDto dto)
        {
            var result = await TryGetValidator<TDto>().ValidateAsync(dto);

            ThrowIfInvalid(result);
        }

        private void ThrowIfInvalid(ValidationResult result)
        {
            if (!result.IsValid)
            {
                var message = string.Join(";  ", result.Errors.Select(x => x.ErrorMessage));
                throw new ValidationException(message);
            }
        }

        private IValidator<TDto> TryGetValidator<TDto>()
        {
            var dtoTypeValidator = _serviceProvider.GetService<IValidator<TDto>>();
            if (dtoTypeValidator is null)
            {
                throw new NoExistingValidatorForGivenTypeException(typeof(TDto));
            }
            return dtoTypeValidator;
        }
    }
}
