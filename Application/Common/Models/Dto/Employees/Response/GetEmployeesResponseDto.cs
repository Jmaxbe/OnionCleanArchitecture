using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Common.Models.Dto.Employees.Response;

public class GetEmployeesResponseDto : IMapFrom<Employee>
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