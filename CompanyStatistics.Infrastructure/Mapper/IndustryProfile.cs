using AutoMapper;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.Domain.DTOs.Organization;

namespace CompanyStatistics.Infrastructure.Mapper
{
    public class IndustryProfile : Profile
    {
        public IndustryProfile()
        {
            CreateMap<OrganizationDto, IndustryRequestDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Industry));
        }
    }
}
