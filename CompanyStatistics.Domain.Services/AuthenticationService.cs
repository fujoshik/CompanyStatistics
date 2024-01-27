using CompanyStatistics.Domain.Abstraction.Providers;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Authentication;

namespace CompanyStatistics.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IValidationProvider _validationProvider;

        public AuthenticationService(IUnitOfWork unitOfWork,
                                     IPasswordService passwordService,
                                     ITokenService tokenService,
                                     IUserService userService,
                                     IAccountService accountService,
                                     IValidationProvider validationProvider)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _userService = userService;
            _accountService = accountService;
            _validationProvider = validationProvider;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            await _validationProvider.TryValidateAsync(loginDto);

            var accounts = await _unitOfWork.AccountRepository.GetAccountsByEmail(loginDto.Email);

            if (accounts == null || accounts.Count == 0)
            {
                return null;
            }

            var user = accounts.FirstOrDefault();

            if (!_passwordService.VerifyPassword(loginDto.Password, user.PasswordHash,
                Convert.FromBase64String(user.PasswordSalt)))
            {
                return null;
            }

            var token = _tokenService.GenerateJwtToken(user);

            return token;
        }

        public async Task RegisterAccountAsync(RegisterDto registerDto)
        {
            await _validationProvider.TryValidateAsync(registerDto);

            var account = await _accountService.CreateAsync(registerDto);

            await _userService.CreateAsync(registerDto, account.Id);
        }
    }
}
