using System.ComponentModel.DataAnnotations;
using Domain.Entities.Dictionaries;

namespace Domain.Entities;

public class Salary : BaseAuditableEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public double Rate { get; set; }
    public Guid DictPostId { get; set; }
    public DictPost DictPost { get; set; }
}