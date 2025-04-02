using AMS_News.Domain.Entities;

namespace AMS_News.Domain.Contracts.User;

public interface IUserUpdateOnlyRepository
{
    void Update(Customers customers);
}