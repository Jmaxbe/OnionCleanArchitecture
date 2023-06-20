namespace Domain.Entities.Dictionaries;

public class DictWorkingDateType : BaseAuditableEntity
{
    public DictWorkingDateType(string name)
    {
        Name = name;
        FlowOfWorks = new HashSet<FlowOfWorks>();
    }

    public string Name { get; init; }

    public ICollection<FlowOfWorks> FlowOfWorks { get; set; }
}