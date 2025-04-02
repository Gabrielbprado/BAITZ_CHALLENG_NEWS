using AMS_News.Application.UseCases.User.Register;
using AMS_News.Communication.Request.User;
using AMS_News.Communication.Response;
using AMS_News.Communication.Response.Token;
using AMS_News.Communication.Response.User;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Contracts.User;
using AMS_News.Domain.Entities;
using AMS_News.Exceptions;
using AMS_News.Exceptions.BaseExceptions;
using AutoMapper;
using FluentValidation.Results;

namespace AMS_News.Application.UseCases.User.Registrer;

public class RegisterUserUseCase(IUserWriteOnlyRepository repository, IPasswordEncrypter passwordEncrypter, IUserReadOnlyRepository userReadOnly, IUnityOfWork unityOfWork, IMapper mapper, ITokenGenerator tokenGenerator) : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _repository = repository;
    private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
    private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        request.Password = _passwordEncrypter.Encrypt(request.Password);
        var user = _mapper.Map<Customers>(request);
        user.UserIdentifier = Guid.NewGuid();
        await _repository.Add(user);
        await _unityOfWork.Commit();
        return new ResponseRegisterUserJson
        {
            Name = request.Name,
            Token = new ResponseTokenJson()
            {
                AccessToken = _tokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validate = new RegisterUserValidator();
        var result = await validate.ValidateAsync(request);
        var existsByEmail = await _userReadOnly.ExistsByEmail(request.Email);
        if (existsByEmail)
        {
            result.Errors.Add(new ValidationFailure("Email", ErrorMessage.EMAIL_ALREADY_EXISTS));
        }
        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidatorException(errors);
        }
    }
}