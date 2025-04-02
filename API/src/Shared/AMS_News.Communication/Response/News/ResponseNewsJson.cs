namespace AMS_News.Communication.Response.News;

public class ResponseNewsJson
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public IList<ResponseTopicsJson> Topics { get; set; } = [];
    
}