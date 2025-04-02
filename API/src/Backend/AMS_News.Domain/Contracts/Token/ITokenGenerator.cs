namespace AMS_News.Domain.Contracts.Token;

public interface ITokenGenerator
{
    public string Generate(Guid userIdentifier);
}