using System.ComponentModel.DataAnnotations;
using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class FlowOfWorks : BaseAuditableEntity
{
    public DateTime WorkDay { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public Guid DictWorkingDateTypeId { get; set; }
    public DictWorkingDateType DictWorkingDateType { get; set; }
}