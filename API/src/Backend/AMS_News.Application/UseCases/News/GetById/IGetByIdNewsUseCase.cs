using AMS_News.Communication.Response.News;

namespace AMS_News.Application.UseCases.News.GetById;

public interface IGetByIdNewsUseCase
{
    Task<ResponseNewsJson> ExecuteAsync(long id);
}