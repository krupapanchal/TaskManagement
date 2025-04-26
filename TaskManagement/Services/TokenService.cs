using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Models;

namespace TaskManagement.Services
{

    /// <summary>
/// Service for generating JSON Web Tokens (JWTs) for user authentication.
/// </summary>
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a JWT for the specified user.
    /// </summary>
    /// <param name="user">The user to generate a token for.</param>
    /// <returns>The generated JWT.</returns>
    public string GenerateToken(Users user)
    {
        // Create an array of claims to include in the token
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username), // Claim for the user's username
            new Claim(ClaimTypes.Role, user.Role.ToString()) // Claim for the user's role
        };

        // Create a symmetric security key from the JWT key in the configuration
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // Create signing credentials using the security key and HMAC SHA256 algorithm
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create a new JWT with the specified claims, issuer, audience, and expiration time
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], // Issuer of the token
            audience: _configuration["Jwt:Audience"], // Audience of the token
            claims: claims, // Claims to include in the token
            expires: DateTime.Now.AddHours(1), // Token expiration time (1 hour from now)
            signingCredentials: credentials // Signing credentials for the token
        );

        // Write the token to a string using a JWT security token handler
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
}
