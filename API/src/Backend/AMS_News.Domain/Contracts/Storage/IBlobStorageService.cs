using AMS_News.Domain.Entities;

namespace AMS_News.Domain.Contracts.Storage;

public interface IBlobStorageService
{
    Task Upload(Customers user, Stream file, string fileName);
    Task<string> GetUri(Customers user, string fileName);
}