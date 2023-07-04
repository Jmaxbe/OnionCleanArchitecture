using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models.Dto.Employees.Response;

public class CreateEmployeeRequestDto : IMapFrom<Employee>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? UserPhone { get; set; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
}