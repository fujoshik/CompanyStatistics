using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _unitOfWork.UserRepository.InsertAsync<UserRequestDto, UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateAsync(string id, UserRequestDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _unitOfWork.UserRepository.UpdateAsync<UserRequestDto, UserResponseDto>(id, user);
        }

        public async Task<UserResponseDto> GetByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _unitOfWork.UserRepository.GetByIdAsync<UserResponseDto>(id);
        }

        public async Task<PaginatedResult<UserResponseDto>> GetPageAsync(PagingInfo pagingInfo)
        {
            return await _unitOfWork.UserRepository.GetPageAsync<UserResponseDto>(pagingInfo.PageNumber, pagingInfo.PageSize);
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _unitOfWork.UserRepository.DeleteAsync(id);
        }
    }
}
