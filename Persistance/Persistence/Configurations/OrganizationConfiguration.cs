using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        // builder
        //     .OwnsOne(a => a.Address);
        builder
            .Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();
        // builder
        //     .Property(a => a.Address.Country)
        //     .HasMaxLength(256)
        //     .IsRequired();
        // builder
        //     .Property(a => a.Address.City)
        //     .HasMaxLength(256)
        //     .IsRequired();
        // builder
        //     .Property(a => a.Address.Street)
        //     .HasMaxLength(256)
        //     .IsRequired();
        // builder
        //     .Property(a => a.Address.State)
        //     .HasMaxLength(256);
        // builder
        //     .Property(a => a.Address.Building)
        //     .HasMaxLength(256);
    }
}