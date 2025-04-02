namespace AMS_News.Domain.Contracts.News;

public interface INewsDeleteOnlyRepository
{
    void DeleteAsync(Entities.News news);
}