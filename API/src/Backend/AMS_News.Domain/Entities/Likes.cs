namespace AMS_News.Domain.Entities;

public class Likes : BaseEntity
{
    public long NewsId { get; set; }
    public long CustomerId { get; set; }
}