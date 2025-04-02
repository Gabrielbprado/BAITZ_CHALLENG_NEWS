using AMS_News.Communication.Response.News;

namespace AMS_News.Application.UseCases.News.GetByUser;

public interface IGetByUserNewsUseCase
{
    Task<List<ResponseNewsJson>> ExecuteAsync();
}