using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIndustryService _industryService;
        private readonly ICompanyIndustriesService _companyIndustriesService;

        public CompanyService(IMapper mapper, 
                              IUnitOfWork unitOfWork,
                              IIndustryService industryService,
                              ICompanyIndustriesService companyIndustriesService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;         
            _industryService = industryService;
            _companyIndustriesService = companyIndustriesService;
        }

        public async Task<CompanyResponseDto> CreateAsync(CompanyCreateDto company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            var companyDto = _mapper.Map<CompanyRequestDto>(company);

            if (companyDto.Id == null)
            {
                companyDto.Id = Guid.NewGuid().ToString();
            }

            await _industryService.CreateIndustryIfNotExistAsync(companyDto);

            await _companyIndustriesService.CreateCompanyIndustryAsync(companyDto);

            var request = _mapper.Map<CompanyWithouIndustryRequestDto>(companyDto);

            var result = await _unitOfWork.CompanyRepository.InsertAsync(request);

            var responseDto = await AssignIndustriesAsync(result);

            return responseDto;
        }

        public async Task<CompanyResponseDto> UpdateAsync(string id, CompanyWithoutIdDto company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            var companyWithoutIndustry = _mapper.Map<CompanyWithoutIndustryDto>(company);

            // update companyIndustries if there are any changes

            var result = await _unitOfWork.CompanyRepository.UpdateAsync<CompanyWithoutIndustryDto, CompanyWithoutIndustryDto>(
                id, companyWithoutIndustry);

            var responseDto = await AssignIndustriesAsync(result);

            return responseDto;
        }

        public async Task<CompanyResponseDto> GetByIdAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = await _unitOfWork.CompanyRepository.GetByIdAsync<CompanyWithoutIndustryDto>(id);

            var responseDto = await AssignIndustriesAsync(result);

            return responseDto;
        }

        public async Task<PaginatedResult<CompanyResponseDto>> GetPageAsync(PagingInfo pagingInfo)
        {
            var companies = await _unitOfWork.CompanyRepository
                .GetPageAsync<CompanyWithoutIndustryDto>(pagingInfo.PageNumber, pagingInfo.PageSize);

            var result = new List<CompanyResponseDto>();

            foreach (var company in companies.Content)
            {
                var companyResponse = await AssignIndustriesAsync(company);
                result.Add(companyResponse);
            }

            return result.Paginate(0, 10);
        }

        public async Task DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            await _unitOfWork.CompanyRepository.DeleteCompanyAsync(id);
        }

        public async Task<CompanyResponseDto> GetCompanyByNameAsync(string companyName)
        {
            var company = await _unitOfWork.CompanyRepository.GetCompanyByNameAsync(companyName);

            return await AssignIndustriesAsync(company);
        }

        public async Task<CompanyResponseDto> AssignIndustriesAsync(CompanyWithoutIndustryDto companyWithoutIndustry)
        {
            var responseDto = _mapper.Map<CompanyResponseDto>(companyWithoutIndustry);

            var industries = await _unitOfWork.CompanyIndustriesRepository.GetIndustriesByCompanyIdAsync(companyWithoutIndustry.Id);
            responseDto.Industries = industries;

            return responseDto;
        }
    }
}
