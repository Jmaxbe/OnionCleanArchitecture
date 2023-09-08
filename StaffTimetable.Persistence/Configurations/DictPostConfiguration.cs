using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities.Dictionaries;

namespace StaffTimetable.Persistence.Configurations;

public class DictPostConfiguration : IEntityTypeConfiguration<DictPost>
{
    public void Configure(EntityTypeBuilder<DictPost> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}