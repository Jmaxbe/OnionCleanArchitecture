﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Persistence.Configurations;

public class WorkDepartmentsConfigure : IEntityTypeConfiguration<WorkDepartments>
{
    public void Configure(EntityTypeBuilder<WorkDepartments> builder)
    {
        builder
            .Property(p => p.OrganizationId)
            .IsRequired();
        builder
            .HasOne(o => o.Organization)
            .WithMany(m => m.WorkDepartments)
            .HasForeignKey(f => f.OrganizationId);
        builder
            .Property(p => p.DictWorkDepartmentsId)
            .IsRequired();
        builder
            .HasOne(o => o.DictWorkDepartments)
            .WithMany(m => m.WorkDepartments)
            .HasForeignKey(f => f.DictWorkDepartmentsId);
    }
}