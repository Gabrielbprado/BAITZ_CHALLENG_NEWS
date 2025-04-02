using AMS_News.Domain.Entities;

namespace AMS_News.Domain.Contracts.Token;

public interface ILoggedUser
{
    Task<Customers?> User();
}