using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Persistence.Configurations;

public class ManagedOrganizationsConfiguration : IEntityTypeConfiguration<ManagedOrganizations>
{
    public void Configure(EntityTypeBuilder<ManagedOrganizations> builder)
    {
        builder.Property(p => p.OrganizationId)
            .IsRequired();
        builder.Property(p => p.EmployeeId)
            .IsRequired();
        builder
            .HasOne(o => o.Organization)
            .WithMany(m => m.ManagedOrganizations)
            .HasForeignKey(f => f.OrganizationId)
            .HasPrincipalKey(g => g.UniqueId);
        builder
            .HasOne(o => o.Employee)
            .WithMany(m => m.ManagedOrganizations)
            .HasForeignKey(f => f.EmployeeId)
            .HasPrincipalKey(g => g.UniqueId);
    }
}