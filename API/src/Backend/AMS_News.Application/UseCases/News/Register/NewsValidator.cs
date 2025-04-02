using AMS_News.Communication.Request.News;
using FluentValidation;

namespace AMS_News.Application.UseCases.News.Register;

public class NewsValidator : AbstractValidator<RequestNewsJson>
{
    public NewsValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required");
    }
}