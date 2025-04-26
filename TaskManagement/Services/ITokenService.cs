using TaskManagement.Models;

namespace TaskManagement.Services
{
    /// <summary>
    /// Interface for token generation services.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a token for the given user.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <returns>The generated token.</returns>
        string GenerateToken(Users user);
    }
}
