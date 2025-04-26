using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    /// <summary>
    /// Controller for handling authentication-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // Database context for interacting with the tasks database
        private readonly TaskDbContext _taskDbContext;

        // Token service instance for generating and validating tokens
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="taskDbContext">The tasks database context instance.</param>
        /// <param name="tokenService">The token service instance.</param>
        public AuthController(TaskDbContext taskDbContext, ITokenService tokenService)
        {
            _taskDbContext = taskDbContext;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Handles the login request.
        /// </summary>
        /// <param name="users">The user credentials.</param>
        /// <returns>A JSON response containing the generated token, or an Unauthorized response if the credentials are invalid.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] Users users)
        {
            // Retrieve the user from the database based on the provided credentials
            var user = _taskDbContext.Users.FirstOrDefault(u => u.Username == users.Username && u.PasswordHash == users.PasswordHash);

            // If no user is found, return an Unauthorized response
            if (user == null)
                return Unauthorized("Invalid credentials");

            // Generate a token for the authenticated user
            var token = _tokenService.GenerateToken(user);

            // Return a JSON response containing the generated token
            return Ok(new { token });
        }
    }
}
