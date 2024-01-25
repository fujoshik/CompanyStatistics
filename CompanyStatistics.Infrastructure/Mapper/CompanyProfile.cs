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
            CreateMap<CompanyCreateDto, CompanyRequestDto>();

            CreateMap<CompanyRequestDto, CompanyWithouIndustryRequestDto>();

            CreateMap<OrganizationDto, CompanyRequestDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.OrganizationId))
                .ForMember(dest => dest.CompanyIndex, opt => opt.MapFrom(x => x.Index));

            CreateMap<Company, CompanyResponseDto>();

            CreateMap<CompanyWithoutIdDto, CompanyWithoutIndustryDto>();

            CreateMap<CompanyWithoutIndustryDto, CompanyResponseDto>();
        }
    }
}
