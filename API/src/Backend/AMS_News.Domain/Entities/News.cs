using System.Diagnostics;

namespace AMS_News.Domain.Entities;

public class News : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public IList<Topics> Topics { get; set; } = [];
    public string? ImageIdentifier { get; set; } = string.Empty;
    public long CustomerId { get; set; }
    
}