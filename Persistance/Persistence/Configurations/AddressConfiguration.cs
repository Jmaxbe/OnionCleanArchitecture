using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(p => p.OrganizationId)
            .IsRequired();
        builder
            .HasOne(o => o.Organization)
            .WithMany(m => m.Addresses)
            .HasForeignKey(f => f.OrganizationId)
            .HasPrincipalKey(g => g.UniqueId);
        builder.Property(p => p.City)
            .HasMaxLength(256);
        builder.Property(p => p.Country)
            .HasMaxLength(256);
        builder.Property(p => p.State)
            .HasMaxLength(256);
        builder.Property(p => p.Street)
            .HasMaxLength(256);
        builder.Property(p => p.Building)
            .HasMaxLength(11);
        builder.Property(p => p.PostalCode)
            .HasMaxLength(12);
    }
}