using AMS_News.Communication.Request.News;

namespace AMS_News.Application.UseCases.News.Edit;

public interface IEditNewsUseCase
{
    Task ExecuteAsync(long id, RequestNewsJson request);
}