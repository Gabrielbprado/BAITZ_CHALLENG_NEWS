namespace AMS_News.Domain.Entities;

public class Customers : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; } = Guid.NewGuid();
}