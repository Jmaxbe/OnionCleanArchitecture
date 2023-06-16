using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(k=>k.Id);
        builder.Property(f => f.FirstName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(f => f.LastName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(f => f.MiddleName)
            .HasMaxLength(256);
    }
}