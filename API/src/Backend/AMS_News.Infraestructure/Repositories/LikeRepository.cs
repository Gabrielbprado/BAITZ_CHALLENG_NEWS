using AMS_News.Domain.Contracts.Like;
using AMS_News.Domain.Entities;
using AMS_News.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AMS_News.Infraestructure.Repositories;

public class LikeRepository(AmsNewsContext context) : ILikeWriteOnlyRepository,ILikeReadOnlyRepository
{
    private readonly AmsNewsContext _context = context;
    public async Task AddAsync(Likes like) => await _context.Likes.AddAsync(like);
    public async Task<long> GetMoreLikedAsync()
    {
       return await context.Likes
            .GroupBy(l => l.NewsId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefaultAsync();
    }
}