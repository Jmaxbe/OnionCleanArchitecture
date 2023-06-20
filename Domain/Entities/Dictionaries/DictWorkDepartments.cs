namespace Domain.Entities.Dictionaries;

public class DictWorkDepartments : BaseAuditableEntity
{
    public DictWorkDepartments(string name)
    {
        Name = name;
    }
    public string Name { get; init; }

    public ICollection<WorkDepartments> WorkDepartments { get; set; }
}