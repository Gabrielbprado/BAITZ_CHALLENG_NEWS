namespace AMS_News.Domain.Contracts.Token;

public interface ITokenValidator
{
    Guid ValidateTokenAndReturnUserIdentifier(string token);
}