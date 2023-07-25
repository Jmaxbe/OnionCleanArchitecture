using Application.Common.Models.Dto.Employees;
using Application.Common.Models.Dto.Employees.Response;
using MediatR;

namespace Application.Employees.Commands.UpdateEmployee;

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