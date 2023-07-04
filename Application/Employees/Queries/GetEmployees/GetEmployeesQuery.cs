using Application.Common.Models.Dto.Employees.Response;
using MediatR;

namespace Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery : IRequest<List<GetEmployeesResponseDto>>
{ 
    
}