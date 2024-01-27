using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Factories;
using CompanyStatistics.Domain.Abstraction.Providers;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.CompanyIndustry;
using CompanyStatistics.Domain.DTOs.Industry;
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
        private readonly ICompanyIndustryFactory _companyIndustryFactory;
        private readonly IValidationProvider _validationProvider;

        public CompanyService(IMapper mapper, 
                              IUnitOfWork unitOfWork,
                              IIndustryService industryService,
                              ICompanyIndustriesService companyIndustriesService,
                              ICompanyIndustryFactory companyIndustryFactory,
                              IValidationProvider validationProvider)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;         
            _industryService = industryService;
            _companyIndustriesService = companyIndustriesService;
            _companyIndustryFactory = companyIndustryFactory;
            _validationProvider = validationProvider;
        }

        public async Task<CompanyResponseDto> CreateAsync(CompanyCreateDto company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            await _validationProvider.TryValidateAsync(company);

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

            await _validationProvider.TryValidateAsync(company);

            var companyWithoutIndustry = _mapper.Map<CompanyWithoutIndustryDto>(company);

            await UpdateCompanyIndustriesAsync(id, company);

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

        private async Task UpdateCompanyIndustriesAsync(string id, CompanyWithoutIdDto company)
        {
            var industries = await _unitOfWork.CompanyIndustriesRepository.GetIndustriesByCompanyIdAsync(id);

            foreach (var industry in company.Industries)
            {
                if (!industries.Select(x => x.Name).Contains(industry.Name))
                {
                    await _unitOfWork.CompanyIndustriesRepository.DeleteByCompanyIdAndIndustryNameAsync(id, industry.Name);
                }
            }

            var newCompanyIndustries = company.Industries
                .Where(x => !industries.Select(i => i.Name).Contains(x.Name))
                .ToList();

            var result = CreateCompanyIndustries(id, newCompanyIndustries);

            await _unitOfWork.CompanyIndustriesRepository.BulkInsertAsync(result);
        }

        private List<CompanyIndustryRequestDto> CreateCompanyIndustries(string companyId, List<IndustryRequestDto> industries)
        {
            var result = new List<CompanyIndustryRequestDto>();

            foreach (var item in industries)
            {
                var companyIndustry = _companyIndustryFactory.CreateCompanyIndustryRequestDto(companyId, item.Name);

                result.Add(companyIndustry);
            }

            return result;
        }
    }
}
