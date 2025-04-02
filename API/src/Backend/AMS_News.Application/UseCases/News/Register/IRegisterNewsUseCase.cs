using AMS_News.Communication.Request.News;
using AMS_News.Communication.Response.News;
using Microsoft.AspNetCore.Http;

namespace AMS_News.Application.UseCases.News.Register;

public interface IRegisterNewsUseCase
{
    Task<ResponseRegiteredNewsJson> Execute(RequestNewsJson request);
}