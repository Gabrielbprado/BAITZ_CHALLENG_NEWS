namespace AMS_News.Domain.Contracts.News;

public interface INewsReadOnlyRepository
{
    Task<IEnumerable<Entities.News>> GetNews();
    Task<Entities.News> GetNewsById(long id);
    Task<List<Entities.News>> GetNewsByUser(long userId);
}