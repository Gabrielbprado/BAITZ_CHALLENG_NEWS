namespace AMS_News.Application.UseCases.News.Delete;

public interface IDeleteNewsUseCase
{
    Task ExecuteAsync(long id);
}