using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeDto : IMapFrom<Employee>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string FullName { get; set; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
    public string TestMapper { get; set; }
}