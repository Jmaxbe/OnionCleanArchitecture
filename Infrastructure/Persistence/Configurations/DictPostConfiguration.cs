using Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DictPostConfiguration : IEntityTypeConfiguration<DictPost>
{
    public void Configure(EntityTypeBuilder<DictPost> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}