using Microsoft.AspNetCore.Http;

namespace AMS_News.Communication.Request.News;

public class RequestNewsJson
{
    public string Title { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public List<RequestTopicsJson> Topics { get; set; } = [];
}