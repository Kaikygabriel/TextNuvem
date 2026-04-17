using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TextNuvem.Application.Services;
using TextNuvem.Domain.BackOffice.Entities;
using TextNuvem.Infra.Configure;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TextNuvem.Infra.Services;

internal sealed class TokenService : ITokenService
{
    private readonly IOptions<ConfigurationJwt> _options;

    public TokenService(IOptions<ConfigurationJwt> options)
    {
        _options = options;
    }

    public string GenerateAccessToken(Customer customer)
    {
        var key = Encoding.UTF8.GetBytes(_options.Value.Key);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(GetClaimsByCustomer(customer)),
            Expires = DateTime.UtcNow.AddHours(_options.Value.HoursExpired)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token); 
    }

    private IEnumerable<Claim> GetClaimsByCustomer(Customer customer)
        => new List<Claim>
        {
            new Claim(ClaimTypes.Name,customer.Id.ToString()),
            new Claim(ClaimTypes.Email,customer.Email.Address),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };
    
    
    public string GenerateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}