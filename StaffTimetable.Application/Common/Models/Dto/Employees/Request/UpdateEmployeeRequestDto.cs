using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Application.Common.Models.Dto.Employees.Request;

public class UpdateEmployeeRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? UserPhone { get; set; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
}