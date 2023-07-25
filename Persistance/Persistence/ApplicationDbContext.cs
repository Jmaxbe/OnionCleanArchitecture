using System.Reflection;
using Domain.Entities;
using Domain.Entities.Dictionaries;
using Infrastructure.Extensions;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly IMediator _mediator;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IMediator mediator)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        _mediator = mediator;
    }

    #region Dicts

    public DbSet<DictPost> DictPost => Set<DictPost>();
    public DbSet<DictWorkDepartments> DictWorkDepartments => Set<DictWorkDepartments>();
    public DbSet<DictWorkingDateType> DictWorkingDateType => Set<DictWorkingDateType>();

    #endregion
    
    public DbSet<Employee> Employee => Set<Employee>();
    public DbSet<FlowOfWorks> FlowOfWorks => Set<FlowOfWorks>();
    public DbSet<ManagedOrganizations> ManagedOrganizations => Set<ManagedOrganizations>();
    public DbSet<Organization> Organization => Set<Organization>();
    public DbSet<Salary> Salary => Set<Salary>();
    public DbSet<WorkDepartments> WorkDepartments => Set<WorkDepartments>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}