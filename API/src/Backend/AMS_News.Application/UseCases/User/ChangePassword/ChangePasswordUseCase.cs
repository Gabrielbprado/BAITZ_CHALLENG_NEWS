using AMS_News.Communication.Request.User;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Contracts.User;
using AMS_News.Exceptions;
using AMS_News.Exceptions.BaseExceptions;
using FluentValidation.Results;

namespace AMS_News.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase(IPasswordEncrypter passwordEncrypter,IUserUpdateOnlyRepository userUpdateOnlyRepository,
    ILoggedUser user,
    IUserReadOnlyRepository userReadOnlyRepository,
    IUnityOfWork unityOfWork) : IChangePasswordUseCase
{
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository = userUpdateOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter= passwordEncrypter;
    private readonly ILoggedUser _user = user;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    public async Task Execute(RequestChangePasswordJson request)
    {
        var user = await _user.User();
        await Validate(request,user.Password);
        var userEntity = await _userReadOnlyRepository.GetById(user.Id);
        userEntity.Password = _passwordEncrypter.Encrypt(request.NewPassword);
        _userUpdateOnlyRepository.Update(userEntity);
        await _unityOfWork.Commit();
    }

    private async Task Validate(RequestChangePasswordJson request,string currentPassword)
    {
        var validate = new ChangePasswordValidator();
        var result = await validate.ValidateAsync(request);
        if (_passwordEncrypter.Verify(request.CurrentPassword,currentPassword) is false)
        {
            result.Errors.Add(new ValidationFailure("IncorrectPassword",ErrorMessage.INCORRECT_PASSWORD));
        }
        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidatorException(errors);
        }
    }
}