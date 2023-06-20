using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Persistence.Configurations;

public class WorkDepartmentsConfigure : IEntityTypeConfiguration<WorkDepartments>
{
    public void Configure(EntityTypeBuilder<WorkDepartments> builder)
    {
        builder
            .Property(p => p.OrganizationId)
            .IsRequired();
        builder
            .HasOne(o => o.Organization)
            .WithMany(m => m.WorkDepartments)
            .HasForeignKey(f => f.OrganizationId)
            .HasPrincipalKey(g => g.UniqueId);
        builder
            .Property(p => p.DictWorkDepartmentsId)
            .IsRequired();
        builder
            .HasOne(o => o.DictWorkDepartments)
            .WithMany(m => m.WorkDepartments)
            .HasForeignKey(f => f.DictWorkDepartmentsId)
            .HasPrincipalKey(g => g.UniqueId);
    }
}