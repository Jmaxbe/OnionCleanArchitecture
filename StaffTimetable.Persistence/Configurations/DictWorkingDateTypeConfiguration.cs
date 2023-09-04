using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities.Dictionaries;

namespace StaffTimetable.Infrastructure.Configurations;

public class DictWorkingDateTypeConfiguration : IEntityTypeConfiguration<DictWorkingDateType>
{
    public void Configure(EntityTypeBuilder<DictWorkingDateType> builder)
    {
        builder
            .Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}