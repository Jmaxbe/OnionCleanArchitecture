using MediatR;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;

namespace StaffTimetable.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest<UpdateEmployeeResponseDto>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string? UserPhone { get; init; }
    public bool IsMale { get; init; }
    public DateTime HireDate { get; init; }
    public DateTime BirthDate { get; init; }
}