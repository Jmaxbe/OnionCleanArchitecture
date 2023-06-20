using Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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