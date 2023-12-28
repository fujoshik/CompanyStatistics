using AutoMapper;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Organization;
using CompanyStatistics.Infrastructure.Entities;

namespace CompanyStatistics.Infrastructure.Mapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<OrganizationDto, CompanyRequestDto>();
            CreateMap<Company, CompanyResponseDto>();
        }
    }
}
