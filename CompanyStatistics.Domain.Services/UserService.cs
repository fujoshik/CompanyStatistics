using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Providers;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidationProvider _validationProvider;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IValidationProvider validationProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationProvider = validationProvider;
        }

        public async Task<UserResponseDto> CreateAsync(RegisterDto registerDto, string accountId)
        {
            if (registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            var user = _mapper.Map<UserRequestDto>(registerDto);
            user.AccountId = accountId;

            return await _unitOfWork.UserRepository.InsertAsync<UserRequestDto, UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateAsync(string id, UserWithoutIdDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _validationProvider.TryValidateAsync(user);

            var userRequestDto = _mapper.Map<UserRequestDto>(user);

            return await _unitOfWork.UserRepository.UpdateAsync<UserRequestDto, UserResponseDto>(id, userRequestDto);
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
