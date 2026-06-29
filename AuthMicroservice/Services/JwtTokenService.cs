using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthMicroservice.Services;

public interface IJwtTokenService
{
    string GenerateEncryptedToken(string username);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateEncryptedToken(string username)
    {
        var signingKeyString = _configuration["JwtConfig:SigningKey"]!;
        var encryptionKeyString = _configuration["JwtConfig:EncryptionKey"]!;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKeyString));
        var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKeyString));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var encryptingCredentials = new EncryptingCredentials(
            encryptionKey,
            SecurityAlgorithms.Aes256KW,
            SecurityAlgorithms.Aes128CbcHmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}