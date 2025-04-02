using AMS_News.Communication.Request.User;

namespace AMS_News.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}