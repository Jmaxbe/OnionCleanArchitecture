using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Common;

namespace StaffTimetable.Persistence.Configurations;

public class BaseAuditableEntityConfiguration : IEntityTypeConfiguration<BaseAuditableEntity>
{
    public void Configure(EntityTypeBuilder<BaseAuditableEntity> builder)
    {
        builder.Property(p => p.Created)
            .IsRequired();
        
        builder.Property(p => p.RowVersion)
            .IsRowVersion()
            .IsRequired();
        
        builder.Property(p => p.CreatedBy)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(p => p.LastModifiedBy)
            .HasMaxLength(256)
            .IsRequired();
    }
}