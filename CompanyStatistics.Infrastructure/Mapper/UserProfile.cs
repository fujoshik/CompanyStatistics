using AutoMapper;
using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.Domain.DTOs.User;

namespace CompanyStatistics.Infrastructure.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, UserRequestDto>();

            CreateMap<UserWithoutIdDto, UserRequestDto>();

            CreateMap<UserCreateDto, UserRequestDto>();
        }
    }
}
