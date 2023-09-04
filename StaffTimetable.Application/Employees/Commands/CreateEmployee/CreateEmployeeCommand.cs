using MediatR;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;

namespace StaffTimetable.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<CreateEmployeeResponseDto>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string? UserPhone { get; init; }
    public string? Email { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public List<string> UserRoles { get; init; }
    public bool IsMale { get; init; }
    public DateTime HireDate { get; init; }
    public DateTime BirthDate { get; init; }
}