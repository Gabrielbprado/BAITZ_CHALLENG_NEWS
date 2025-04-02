using AMS_News.Communication.Request.User;
using AMS_News.Communication.Response.User;

namespace AMS_News.Application.UseCases.User.Registrer;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson requestRegisterUserJson);
}