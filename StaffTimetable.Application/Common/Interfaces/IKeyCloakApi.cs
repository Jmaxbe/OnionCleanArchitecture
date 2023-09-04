using StaffTimetable.Application.Common.Models.Dto.Keycloak.Request;

namespace StaffTimetable.Application.Common.Interfaces;

public interface IKeyCloakApi
{
    Task<Guid> CreateUser(CreateUserDto user);
}