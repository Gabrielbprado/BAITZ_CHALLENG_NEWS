namespace AMS_News.Domain.Contracts;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string password,string hash);
}