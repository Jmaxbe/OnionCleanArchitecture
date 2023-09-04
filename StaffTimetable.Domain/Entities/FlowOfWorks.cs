using StaffTimetable.Domain.Common;
using StaffTimetable.Domain.Entities.Dictionaries;

namespace StaffTimetable.Domain.Entities;

public class FlowOfWorks : BaseAuditableEntity
{
    public DateTime WorkDay { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public Guid DictWorkingDateTypeId { get; set; }
    public DictWorkingDateType DictWorkingDateType { get; set; }
}