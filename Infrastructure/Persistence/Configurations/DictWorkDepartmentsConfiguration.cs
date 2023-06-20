using Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Persistence.Configurations;

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