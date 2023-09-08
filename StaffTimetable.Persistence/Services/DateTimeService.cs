using StaffTimetable.Application.Common.Interfaces;

namespace StaffTimetable.Persistence.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}