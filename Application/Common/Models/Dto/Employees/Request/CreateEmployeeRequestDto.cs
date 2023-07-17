using Application.Common.Mappings;
using Application.Employees.Commands.CreateEmployee;
using Domain.Entities;

namespace Application.Common.Models.Dto.Employees.Response;

public class CreateEmployeeRequestDto : IMapFrom<CreateEmployeeCommand>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public List<string> UserRoles { get; set; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public bool IsMale { get; init; }
    public DateTime HireDate { get; init; }
    public DateTime BirthDate { get; init; }
}