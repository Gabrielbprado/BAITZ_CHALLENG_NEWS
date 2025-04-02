
namespace AMS_News.Domain.Contracts.Like;

public interface ILikeReadOnlyRepository
{
    Task<long> GetMoreLikedAsync();
}