namespace AMS_News.Exceptions.BaseExceptions;

public class ErrorOnValidatorException(IList<string> errorMessages) : NewsException(string.Empty)
{
    public IList<string> ErrorMessage { get; set; } = errorMessages;
}