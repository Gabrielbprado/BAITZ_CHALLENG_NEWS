using AMS_News.Domain.Contracts.News;
using AMS_News.Domain.Entities;
using AMS_News.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AMS_News.Infraestructure.Repositories;

public class NewsRepository(AmsNewsContext context) : INewsReadOnlyRepository,INewsWriteOnlyRepository,INewsDeleteOnlyRepository
{
    private readonly AmsNewsContext _context = context;
    public async Task<IEnumerable<News>> GetNews() => await _context.News.AsNoTracking().Include(n => n.Topics).ToListAsync();
    public async Task AddNews(News news) => await _context.News.AddAsync(news);
    public void UpdateNews(News news) => _context.News.Update(news);
    public async Task<News> GetNewsById(long id) => await _context.News.AsNoTracking().Include(n => n.Topics).FirstAsync(n => n.Id == id);
    public Task<List<News>> GetNewsByUser(long userId) => _context.News.AsNoTracking().Include(n => n.Topics).Where(n => n.CustomerId == userId).ToListAsync();
    public void DeleteAsync(News news) => _context.News.Remove(news);
}