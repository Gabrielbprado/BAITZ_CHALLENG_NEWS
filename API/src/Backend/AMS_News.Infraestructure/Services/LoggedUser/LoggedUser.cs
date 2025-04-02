using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AMS_News.Domain.Contracts.Token;
using AMS_News.Domain.Entities;
using AMS_News.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AMS_News.Infraestructure.Services.LoggedUser;

public class LoggedUser(AmsNewsContext context,ITokenProvider provider) : ILoggedUser
{
    private readonly AmsNewsContext _context = context;
    private readonly ITokenProvider _provider = provider;
    public async Task<Customers?> User()
    {
        var token = _provider.Value();
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userIdentifier = Guid.Parse(jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(u => u.UserIdentifier == userIdentifier)!;
    }
}