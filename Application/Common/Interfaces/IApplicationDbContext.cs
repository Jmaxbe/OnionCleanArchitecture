using Domain.Entities;
using Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Employee> Employee { get; }
    DbSet<FlowOfWorks> FlowOfWorks { get; }
    DbSet<ManagedOrganizations> ManagedOrganizations { get; }
    DbSet<Organization> Organization { get; }
    DbSet<WorkDepartments> WorkDepartments { get; }
    DbSet<Salary> Salary { get; }
    DbSet<DictPost> DictPost { get; }
    DbSet<DictWorkDepartments> DictWorkDepartments { get; }
    DbSet<DictWorkingDateType> DictWorkingDateType { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}