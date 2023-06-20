using System.ComponentModel.DataAnnotations;
using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class FlowOfWorks : BaseAuditableEntity
{
    [Required] public DateTime WorkDay { get; set; }
    [Required] public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    [Required] public Guid DictWorkingDateTypeId { get; set; }
    public DictWorkingDateType DictWorkingDateType { get; set; }
}