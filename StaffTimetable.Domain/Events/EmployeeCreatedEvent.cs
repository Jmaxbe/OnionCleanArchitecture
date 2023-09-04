using StaffTimetable.Domain.Common;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Domain.Events;

public class EmployeeCreatedEvent : BaseEvent
{
    public EmployeeCreatedEvent(Employee item)
    {
        Item = item;
    }

    public Employee Item { get; }
}