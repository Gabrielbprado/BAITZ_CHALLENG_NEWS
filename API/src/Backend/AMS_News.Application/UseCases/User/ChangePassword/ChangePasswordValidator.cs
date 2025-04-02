using AMS_News.Application.SharedValidators;
using AMS_News.Communication.Request.User;
using FluentValidation;

namespace AMS_News.Application.UseCases.User.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(r => r.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}