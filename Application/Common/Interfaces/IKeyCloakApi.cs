
namespace Application.Common.Interfaces;

public interface IKeyCloakApi
{
    Task<bool> CreateUser();
}