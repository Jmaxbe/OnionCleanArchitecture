using System.ComponentModel.DataAnnotations;
using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class Salary : BaseAuditableEntity
{
    [Required] public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public double Rate { get; set; }
    [Required] public Guid DictPostId { get; set; }
    public DictPost DictPost { get; set; }
}