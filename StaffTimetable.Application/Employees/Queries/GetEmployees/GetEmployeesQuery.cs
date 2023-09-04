using MediatR;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;

namespace StaffTimetable.Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery : IRequest<List<GetEmployeesResponseDto>>
{ 
    
}