using AutoMapper;
using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.DTOs.Authentication;

namespace CompanyStatistics.Infrastructure.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterDto, AccountRequestDto>();
        }
    }
}
