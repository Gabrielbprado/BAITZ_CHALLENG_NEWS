using AMS_News.Domain.Contracts;
using AMS_News.Infraestructure.Data;

namespace AMS_News.Infraestructure.Repositories;

public class UnityOfWork(AmsNewsContext context) : IUnityOfWork, IDisposable
{
    private readonly AmsNewsContext _context = context;
    private bool disposed = false;
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        Dispose(true);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

}