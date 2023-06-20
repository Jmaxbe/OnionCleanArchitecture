namespace Domain.Entities.Dictionaries;

public class DictPost : BaseAuditableEntity
{
    public DictPost(string name)
    {
        Name = name;
        Salaries = new HashSet<Salary>();
    }

    public string Name { get; init; }

    public ICollection<Salary> Salaries { get; set; }
}