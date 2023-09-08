namespace StaffTimetable.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}