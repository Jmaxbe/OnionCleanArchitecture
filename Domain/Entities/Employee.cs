using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee : BaseAuditableEntity
{
    public Employee()
    {
        Salaries = new HashSet<Salary>();
        FlowOfWorks = new HashSet<FlowOfWorks>();
    }

    public Guid ExternalUserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public bool IsMale { get; set; }
    public string? UserEmail { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }

    public string FullName
    {
        get
        {
            var fullName = $"{LastName} {FirstName}";
            if (!string.IsNullOrEmpty(MiddleName)) fullName += $" {MiddleName}";

            return fullName;
        }
    }

    public ICollection<Salary> Salaries { get; set; }
    public ICollection<FlowOfWorks> FlowOfWorks { get; set; }
    public ICollection<ManagedOrganizations>? ManagedOrganizations { get; set; }
}