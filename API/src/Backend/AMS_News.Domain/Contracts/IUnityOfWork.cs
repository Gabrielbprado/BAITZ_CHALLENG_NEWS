namespace AMS_News.Domain.Contracts;

public interface IUnityOfWork
{
    Task Commit();
}