namespace AMS_News.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
    public bool IsActive { get; set; } = true;
}