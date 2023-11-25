using StaffTimetable.Application.Employees.Commands.CreateEmployee;

namespace StaffTimetable.Application.Common.Models.Dto.Employees.Request;

public class CreateEmployeeRequestDto
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