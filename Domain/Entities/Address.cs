namespace Domain.Entities;

public class Address : BaseAuditableEntity
{
    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string? State { get; set; }
    public string Street { get; set; }
    public string? Building { get; set; }
    public int? PostalCode { get; set; }
}