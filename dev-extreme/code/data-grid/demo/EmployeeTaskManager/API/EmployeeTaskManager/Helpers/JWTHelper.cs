using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeTaskManager.Helpers
{
    /// <summary>
    /// Helper class for generating JSON Web Tokens (JWT) for authentication in the EmployeeTaskManager system.
    /// </summary>
    public class JWTHelper
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the JWTHelper class with configuration settings.
        /// </summary>
        /// <param name="config">The configuration containing JWT settings (Key, Issuer, Audience).</param>
        /// <exception cref="ArgumentNullException">Thrown if config is null.</exception>
        public JWTHelper(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Generates a JWT token based on user details.
        /// </summary>
        /// <param name="username">The user's username (e.g., R01F02 from USR01).</param>
        /// <param name="userID">The user's ID (e.g., R01F01 from USR01).</param>
        /// <param name="role">The user's role (e.g., Admin, Manager, Employee from R01F04).</param>
        /// <param name="expirationHours">The token expiration time in hours (default is 1 hour).</param>
        /// <returns>A response object containing the generated JWT token.</returns>
        /// <exception cref="ArgumentException">Thrown if username or role is null/empty, or userID is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if JWT configuration settings are missing or invalid.</exception>
        public object GenerateJWTToken(string username, int userID, string role, int expirationHours = 1)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            if (userID <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userID));
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be null or empty.", nameof(role));

            // Validate configuration
            string jwtKey = _config["Jwt:Key"];
            string jwtIssuer = _config["Jwt:Issuer"];
            string jwtAudience = _config["Jwt:Audience"];

            if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 16) // Minimum key length for HMAC SHA-256
                throw new InvalidOperationException("JWT Key is missing or too short in configuration.");
            if (string.IsNullOrEmpty(jwtIssuer))
                throw new InvalidOperationException("JWT Issuer is missing in configuration.");
            if (string.IsNullOrEmpty(jwtAudience))
                throw new InvalidOperationException("JWT Audience is missing in configuration.");

            // Define the claims for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userID.ToString())
            };

            // Create the security key and credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            try
            {
                // Create the JWT token
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: jwtIssuer,
                    audience: jwtAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(expirationHours),
                    signingCredentials: signingCredentials
                );

                // Serialize the token
                var tokenHandler = new JwtSecurityTokenHandler();
                string token = tokenHandler.WriteToken(jwtSecurityToken);

                return new { Token = token };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to generate JWT token.", ex);
            }
        }
    }
}