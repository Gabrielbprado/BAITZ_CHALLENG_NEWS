using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AMS_News.Domain.Contracts.Token;

namespace AMS_News.Infraestructure.Security.Token.Access.Generate;

public class JwtAccessTokenGenerator(uint expirationTokenInMinutes, string signKey) : JwtHandler,ITokenGenerator
{
    private readonly uint _expirationTokenInMinutes = expirationTokenInMinutes;
    private readonly string _signKey = signKey;

    public string Generate(Guid userIndentifier)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, userIndentifier.ToString())
        };
        var tokenDescriptor = new SecurityTokenDescriptor()
        { 
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTokenInMinutes),
            SigningCredentials = new SigningCredentials(ConvertToSecurityKey(signKey),SecurityAlgorithms.HmacSha256Signature)

        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);

    }
}
