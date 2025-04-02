using AMS_News.Communication.Request.User;

namespace AMS_News.Application.UseCases.User.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}