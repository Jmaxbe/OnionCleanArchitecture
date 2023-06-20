using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class WorkDepartments : BaseAuditableEntity
{
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public Guid DictWorkDepartmentsId { get; set; }
    public DictWorkDepartments DictWorkDepartments { get; set; }
}