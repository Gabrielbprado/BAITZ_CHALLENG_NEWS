using AMS_News.Application.UseCases.News.GetById;
using AMS_News.Communication.Response.News;
using AMS_News.Domain.Contracts.Like;
using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Contracts.Storage;
using AMS_News.Domain.Contracts.User;

namespace AMS_News.Application.UseCases.Like.GetMoreLiked;

public class GetMoreLiked(IUserReadOnlyRepository userReadOnlyRepository,ILikeReadOnlyRepository readOnlyRepository,INewsReadOnlyRepository readOnly,IBlobStorageService blobStorageService) : IGetMoreLiked
{
    private readonly ILikeReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly INewsReadOnlyRepository _readOnly = readOnly;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;


    public async Task<ResponseNewsJson> ExecuteAsync()
    {
        var newsId =  await _readOnlyRepository.GetMoreLikedAsync();
        var news = await _readOnly.GetNewsById(newsId);
        var customer = await _userReadOnlyRepository.GetById(news.CustomerId);
        return new ResponseNewsJson
        {
            Id = news.Id,
            Title = news.Title,
            Description = news.Description,
            Content = news.Content,
            Author = news.Author,
            Introduction = news.Introduction,
            ImageUrl = await _blobStorageService.GetUri(customer, news.ImageIdentifier),
            CreatedAt = news.CreatedAt,
            Topics = news.Topics.Select(x => new ResponseTopicsJson
            {
                Description = x.Description,
                Title = x.Title,
                ImageUrl = x.ImageIdentifier
            }).ToList()
        };
        
    }
}