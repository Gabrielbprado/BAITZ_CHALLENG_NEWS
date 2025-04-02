using AMS_News.Communication.Request.Like;

namespace AMS_News.Application.UseCases.Like.DoLike;

public interface IDoLikeUseCase
{
    Task ExecuteAsync(long request);
}