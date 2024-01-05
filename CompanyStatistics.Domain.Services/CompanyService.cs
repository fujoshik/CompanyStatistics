using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CompanyResponseDto> CreateAsync(CompanyRequestDto companyDto)
        {
            if (companyDto == null)
            {
                throw new ArgumentNullException(nameof(companyDto));
            }

            return await _unitOfWork.CompanyRepository.InsertAsync<CompanyRequestDto, CompanyResponseDto>(companyDto);
        }

        public async Task<CompanyResponseDto> UpdateAsync(string id, CompanyWithoutIdDto company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            var companyRequestDto = _mapper.Map<CompanyRequestDto>(company);

            return await _unitOfWork.CompanyRepository.UpdateAsync<CompanyRequestDto, CompanyResponseDto>(id, companyRequestDto);
        }

        public async Task<CompanyResponseDto> GetByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _unitOfWork.CompanyRepository.GetByIdAsync<CompanyResponseDto>(id);
        }

        public async Task<PaginatedResult<CompanyResponseDto>> GetPageAsync(PagingInfo pagingInfo)
        {
            return await _unitOfWork.CompanyRepository.GetPageAsync<CompanyResponseDto>(pagingInfo.PageNumber, pagingInfo.PageSize);
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _unitOfWork.CompanyRepository.DeleteAsync(id);
        }
    }
}
