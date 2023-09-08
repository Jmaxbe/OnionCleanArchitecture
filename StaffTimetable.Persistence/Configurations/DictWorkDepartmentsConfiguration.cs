using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities.Dictionaries;

namespace StaffTimetable.Persistence.Configurations;

public class DictWorkDepartmentsConfiguration : IEntityTypeConfiguration<DictWorkDepartments>
{
    public void Configure(EntityTypeBuilder<DictWorkDepartments> builder)
    {
        builder
            .Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}