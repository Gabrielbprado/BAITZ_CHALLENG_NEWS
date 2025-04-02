using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMS_News.Infraestructure.Security.Token;

public abstract class JwtHandler
{
    protected static SymmetricSecurityKey ConvertToSecurityKey(string signKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));
    }
}
