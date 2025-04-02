namespace AMS_News.Communication.Response;

public class ResponseErrorMessageJson
{
    public List<string> Message { get; set; }
    public bool TokenIsExpired { get; set; }
    
    public ResponseErrorMessageJson(string message)
    {
        Message = [message];
    } 
    
    public ResponseErrorMessageJson(List<string> message)
    {
        Message = message;
    }
    
    
}