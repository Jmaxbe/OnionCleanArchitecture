using AutoMapper;
using StaffTimetable.Application.Common.Models.Dto.Keycloak.Request;
using StaffTimetable.Application.Employees.Commands.CreateEmployee;

namespace StaffTimetable.Application.Common.Mappings;

public class AuthMappings : Profile
{
    public AuthMappings()
    {
        CreateMap<CreateEmployeeCommand, CreateUserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            ;
    }
}