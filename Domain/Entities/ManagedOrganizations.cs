using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ManagedOrganizations : BaseAuditableEntity
{
    [Required] public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    [Required] public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}