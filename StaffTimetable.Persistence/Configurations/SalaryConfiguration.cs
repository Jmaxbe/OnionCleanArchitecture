﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Persistence.Configurations;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.Property(p => p.EmployeeId)
            .IsRequired();
        builder
            .HasOne(o => o.Employee)
            .WithMany(m => m.Salaries)
            .HasForeignKey(f=>f.EmployeeId);
        builder.Property(p => p.DictPostId)
            .IsRequired();
        builder
            .HasOne(o => o.DictPost)
            .WithMany(m => m.Salaries)
            .HasForeignKey(f => f.DictPostId);
    }
}