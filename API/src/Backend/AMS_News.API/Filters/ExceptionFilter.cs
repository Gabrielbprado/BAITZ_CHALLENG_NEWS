using AMS_News.Communication.Response;
using AMS_News.Exceptions;
using AMS_News.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AMS_News.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is NewsException)
            HandleExceptionProject(context);
      
    }

    private static void ThrowUnknownException(ExceptionContext context)
    {
        context.Result = new BadRequestObjectResult(new ResponseErrorMessageJson(ErrorMessage.UNKNOWN_ERROR));
        context.HttpContext.Response.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
    }

    private static void HandleExceptionProject(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidatorException)
        {
            var exception = context.Exception as ErrorOnValidatorException;
            context.Result = new ObjectResult(new ResponseErrorMessageJson(exception.ErrorMessage.ToList()));
            context.HttpContext.Response.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
        }
        else if (context.Exception is InvalidLoginException)
        {
            var exception = context.Exception as InvalidLoginException;
            context.Result = new ObjectResult(new ResponseErrorMessageJson(exception.Message));
            context.HttpContext.Response.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        else
        {
            context.Result = new ObjectResult(new ResponseErrorMessageJson(context.Exception.Message));
            context.HttpContext.Response.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}