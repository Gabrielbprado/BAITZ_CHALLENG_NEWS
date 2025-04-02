using AMS_News.Communication.Request.User;
using Bogus;

namespace CommunUtilities.Request;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Builder(int password = 10)
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(x => x.Name, f => f.Person.FirstName)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Password, f => f.Internet.Password(password))
            .Generate();
       
    }
}