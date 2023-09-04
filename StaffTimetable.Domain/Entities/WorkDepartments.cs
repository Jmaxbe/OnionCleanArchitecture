using StaffTimetable.Domain.Common;
using StaffTimetable.Domain.Entities.Dictionaries;

namespace StaffTimetable.Domain.Entities;

public class WorkDepartments : BaseAuditableEntity
{
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public Guid DictWorkDepartmentsId { get; set; }
    public DictWorkDepartments DictWorkDepartments { get; set; }
}