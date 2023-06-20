using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Employees.Queries.GetEmployees;

public class GetEmployeesDto : IMapFrom<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string FullName { get; init; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
}