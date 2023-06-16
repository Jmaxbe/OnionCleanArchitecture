namespace Domain.Entities;

public class Employee : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }

    public string FullName
    {
        get
        {
            var fullName = $"{LastName} {FirstName}";
            if (!string.IsNullOrEmpty(MiddleName)) fullName += $" {MiddleName}";

            return fullName;
        }
    }

    public DateTime BirthDay { get; set; }
}