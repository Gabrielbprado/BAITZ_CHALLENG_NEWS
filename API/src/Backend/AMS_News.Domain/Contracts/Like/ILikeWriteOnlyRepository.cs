namespace AMS_News.Domain.Contracts.Like;

public interface ILikeWriteOnlyRepository
{
    Task AddAsync(Entities.Likes like);
}