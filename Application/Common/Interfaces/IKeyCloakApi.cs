
using Application.Common.Models.Dto.Keycloak.Request;

namespace Application.Common.Interfaces;

public interface IKeyCloakApi
{
    Task<Guid> CreateUser(CreateUserDto user);
}