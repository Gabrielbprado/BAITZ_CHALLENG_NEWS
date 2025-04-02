namespace AMS_News.Domain.Entities;

public class Topics : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageIdentifier { get; set; } = string.Empty;
    public long NewsId { get; set; }
}