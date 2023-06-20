namespace Domain.Entities;

public class Organization : BaseAuditableEntity
{
    public Organization(string name, string country, string city, string street)
    {
        Name = name;
        Country = country;
        City = city;
        Street = street;
        ManagedOrganizations = new HashSet<ManagedOrganizations>();
    }

    public string Name { get; init; }
    public string Country { get; init; }
    public string City { get; init; }
    public string? State { get; set; }
    public string Street { get; init; }
    public string? Building { get; set; }
    public int? PostalCode { get; set; }
    
    public ICollection<ManagedOrganizations> ManagedOrganizations { get; set; }
    public ICollection<WorkDepartments> WorkDepartments { get; set; }
}