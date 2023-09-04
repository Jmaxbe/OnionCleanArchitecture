using StaffTimetable.Application.Common.Interfaces;

namespace StaffTimetable.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}