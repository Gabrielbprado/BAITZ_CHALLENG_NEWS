namespace AMS_News.Domain.Contracts.News;

public interface INewsWriteOnlyRepository
{
    Task AddNews(Entities.News news);
    void UpdateNews(Entities.News news);
}