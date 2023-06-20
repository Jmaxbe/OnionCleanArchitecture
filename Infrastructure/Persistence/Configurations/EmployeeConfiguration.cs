using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(f => f.FirstName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(f => f.LastName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(f => f.MiddleName)
            .HasMaxLength(256);
        builder.Property(b => b.BirthDate)
            .IsRequired();
        builder.Property(b => b.HireDate)
            .IsRequired();
    }
}