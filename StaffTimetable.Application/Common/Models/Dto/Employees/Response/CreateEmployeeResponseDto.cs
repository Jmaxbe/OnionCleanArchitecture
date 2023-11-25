using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Application.Common.Models.Dto.Employees.Response;

public class CreateEmployeeResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string FullName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPhone { get; set; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
}