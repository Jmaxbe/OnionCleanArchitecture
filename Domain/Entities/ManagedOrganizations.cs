﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ManagedOrganizations : BaseAuditableEntity
{
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
}