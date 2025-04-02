using Microsoft.AspNetCore.Http;

namespace AMS_News.Communication.Request.News;

public class RequestTopicsJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}