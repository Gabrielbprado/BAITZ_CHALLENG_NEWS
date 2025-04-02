using AMS_News.Communication.Response.News;

namespace AMS_News.Application.UseCases.Like.GetMoreLiked;

public interface IGetMoreLiked
{
    Task<ResponseNewsJson> ExecuteAsync();
}