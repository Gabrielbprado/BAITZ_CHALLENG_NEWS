using AMS_News.Application.Login;
using AMS_News.Communication.Request;
using AMS_News.Communication.Response;
using AMS_News.Communication.Response.Token;
using AMS_News.Communication.Response.User;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Contracts.User;
using AMS_News.Exceptions;
using AMS_News.Exceptions.BaseExceptions;

namespace AMS_News.Application.UseCases.Login;

public class DoLoginUseCase(IPasswordEncrypter encrypter,IUserReadOnlyRepository repository,ITokenGenerator generator) : IDoLoginUseCase
{
    private readonly IPasswordEncrypter _encrypter = encrypter;
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly ITokenGenerator _tokenGenerator = generator;
    
    public async Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request)
    {
        var user = await _repository.GetByEmail(request.Email);
        var verify = user is not null && _encrypter.Verify(request.Password, user.Password) is true; 
        if (verify is false)
        {
            throw new InvalidLoginException(ErrorMessage.INVALID_LOGIN);
        }
        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = new ResponseTokenJson()
            {
                AccessToken = _tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}