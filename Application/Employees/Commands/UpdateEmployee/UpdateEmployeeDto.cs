using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeDto : IMapFrom<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string FullName { get; set; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
}