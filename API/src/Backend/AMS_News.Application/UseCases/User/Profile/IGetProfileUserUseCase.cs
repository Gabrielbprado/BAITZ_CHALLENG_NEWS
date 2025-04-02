using AMS_News.Communication.Response.User;

namespace AMS_News.Application.UseCases.User.Profile;

public interface IGetProfileUserUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
