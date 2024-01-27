using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Providers;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IValidationProvider _validationProvider;

        public AccountService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IPasswordService passwordService,
                              IValidationProvider validationProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordService = passwordService;
            _validationProvider = validationProvider;
        }

        public async Task<AccountResponseDto> CreateAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            var account = _mapper.Map<AccountRequestDto>(registerDto);

            GeneratePassword(account, registerDto);

            return await _unitOfWork.AccountRepository.InsertAsync<AccountRequestDto, AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> UpdateAsync(string id, AccountRequestDto account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            await _validationProvider.TryValidateAsync(account);

            return await _unitOfWork.AccountRepository.UpdateAsync<AccountRequestDto, AccountResponseDto>(id, account);
        }

        public async Task<AccountResponseDto> GetByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _unitOfWork.AccountRepository.GetByIdAsync<AccountResponseDto>(id);
        }

        public async Task<PaginatedResult<AccountResponseDto>> GetPageAsync(PagingInfo pagingInfo)
        {
            return await _unitOfWork.AccountRepository.GetPageAsync<AccountResponseDto>(pagingInfo.PageNumber, pagingInfo.PageSize);
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _unitOfWork.AccountRepository.DeleteAsync(id);
        }

        private void GeneratePassword(AccountRequestDto account, RegisterDto registerDto)
        {
            var salt = _passwordService.GenerateSalt();
            account.PasswordHash = _passwordService.HashPasword(registerDto.Password, salt);
            account.PasswordSalt = Convert.ToBase64String(salt);
        }
    }
}
