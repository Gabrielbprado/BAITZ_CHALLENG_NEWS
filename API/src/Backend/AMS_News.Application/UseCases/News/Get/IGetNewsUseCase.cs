using AMS_News.Communication.Response.News;

namespace AMS_News.Application.UseCases.News.Get;

public interface IGetNewsUseCase
{
    Task<IEnumerable<ResponseNewsJson>> Execute();
}