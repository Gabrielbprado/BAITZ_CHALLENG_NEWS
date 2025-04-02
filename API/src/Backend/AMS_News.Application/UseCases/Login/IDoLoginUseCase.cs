using AMS_News.Communication.Request;
using AMS_News.Communication.Response.User;

namespace AMS_News.Application.Login;

public interface IDoLoginUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request);
}