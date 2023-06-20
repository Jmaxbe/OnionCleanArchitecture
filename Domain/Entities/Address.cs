namespace Domain.Entities;

public class Address
{
    public Address(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;
    }
    
    public string Country { get; init; }
    public string City { get; init; }
    public string? State { get; set; }
    public string Street { get; init; }
    public string? Building { get; set; }
    public int? PostalCode { get; set; }
}