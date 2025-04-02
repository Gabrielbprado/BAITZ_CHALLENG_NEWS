using AMS_News.Communication.Request.User;
using AMS_News.Domain.Contracts;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Contracts.User;
using AMS_News.Exceptions;
using AMS_News.Exceptions.BaseExceptions;
using FluentValidation.Results;

namespace AMS_News.Application.UseCases.User.Update;

public class UpdateUserUseCase(ILoggedUser user,IUserReadOnlyRepository _read,IUserUpdateOnlyRepository update,IUnityOfWork unityOfWork) : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser = user;
    private readonly IUserReadOnlyRepository _read = _read;
    private readonly IUserUpdateOnlyRepository _update = update;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    public async Task Execute(RequestUpdateUserJson request)
    {
        var user = await _loggedUser.User();
        await Validate(request,request.Email);
        var newUser = await _read.GetById(user.Id);
        newUser.Name = request.Name;
        newUser.Email = request.Email;
        _update.Update(newUser);
        await _unityOfWork.Commit();

    }
    
    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validate = new UpdateUserValidator();
        var result = await validate.ValidateAsync(request);
        var existsByEmail = await _read.ExistsByEmail(currentEmail);
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