using AMS_News.Domain.Entities;

namespace AMS_News.Domain.Contracts.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Customers customer);
}