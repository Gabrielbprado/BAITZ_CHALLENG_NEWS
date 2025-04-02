using AMS_News.Application.SharedValidators;
using AMS_News.Communication.Request.User;
using AMS_News.Exceptions;
using FluentValidation;

namespace AMS_News.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ErrorMessage.NAME_EMPTY);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(ErrorMessage.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ErrorMessage.EMAIL_INVALID);

        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}