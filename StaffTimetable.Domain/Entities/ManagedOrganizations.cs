using StaffTimetable.Domain.Common;

namespace StaffTimetable.Domain.Entities;

public class ManagedOrganizations : BaseAuditableEntity
{
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}