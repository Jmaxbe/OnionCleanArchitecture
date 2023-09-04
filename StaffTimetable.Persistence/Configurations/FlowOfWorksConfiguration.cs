using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Persistence.Configurations;

public class FlowOfWorksConfiguration : IEntityTypeConfiguration<FlowOfWorks>
{
    public void Configure(EntityTypeBuilder<FlowOfWorks> builder)
    {
        builder
            .HasOne(o => o.Employee)
            .WithMany(m => m.FlowOfWorks)
            .HasForeignKey(f => f.EmployeeId)
            .HasPrincipalKey(g => g.UniqueId);
        builder
            .HasOne(o => o.DictWorkingDateType)
            .WithMany(m => m.FlowOfWorks)
            .HasForeignKey(f => f.DictWorkingDateTypeId)
            .HasPrincipalKey(g => g.UniqueId);
        builder.Property(d => d.WorkDay)
            .IsRequired();
    }
}