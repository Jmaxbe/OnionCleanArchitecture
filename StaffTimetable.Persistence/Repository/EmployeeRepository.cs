using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Persistence.Repository;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}