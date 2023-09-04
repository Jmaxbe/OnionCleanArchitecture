using StaffTimetable.Domain.Common;

namespace StaffTimetable.Domain.Entities;

public class Organization : BaseAuditableEntity
{
    public Organization()
    {
        ManagedOrganizations = new HashSet<ManagedOrganizations>();
        Addresses = new HashSet<Address>();
    }

    public string Name { get; init; }

    public ICollection<ManagedOrganizations> ManagedOrganizations { get; set; }
    public ICollection<WorkDepartments> WorkDepartments { get; set; }
    public ICollection<Address> Addresses { get; set; }
}