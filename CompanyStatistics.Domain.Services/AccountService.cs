using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccountResponseDto> CreateAsync(AccountRequestDto account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return await _unitOfWork.AccountRepository.InsertAsync<AccountRequestDto, AccountResponseDto>(account);
        }

        public async Task<AccountResponseDto> UpdateAsync(string id, AccountRequestDto account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

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
    }
}
