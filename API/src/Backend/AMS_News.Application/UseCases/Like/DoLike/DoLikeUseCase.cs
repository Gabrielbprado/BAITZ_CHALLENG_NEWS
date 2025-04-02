using AMS_News.Communication.Request.Like;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Like;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Entities;

namespace AMS_News.Application.UseCases.Like.DoLike;

public class DoLikeUseCase(ILikeWriteOnlyRepository likeWriteOnlyRepository,ILoggedUser loggedUser,IUnityOfWork unityOfWork) : IDoLikeUseCase
{
    private readonly ILikeWriteOnlyRepository _likeWriteOnlyRepository = likeWriteOnlyRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    public async Task ExecuteAsync(long id)
    {
        var user = await _loggedUser.User();
        var like = new Likes
        {
             NewsId= id,
            CustomerId = user.Id
        };
        await _likeWriteOnlyRepository.AddAsync(like);
        await _unityOfWork.Commit();
    }
}