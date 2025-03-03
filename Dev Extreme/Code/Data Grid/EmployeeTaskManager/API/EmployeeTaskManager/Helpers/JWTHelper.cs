using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeTaskManager.Helpers
{
    public class JWTHelper
    {
        private readonly IConfiguration _config;

        public JWTHelper(IConfiguration config)
        {
            _config = config;
        }

        public object GenerateJWTToken(string username, int userID, string role, int expirationHours = 1)
        {
            // Define the claims for the token
            Claim[] arrClaims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userID.ToString())
            };

            // Create the security key and credentials
            SymmetricSecurityKey objSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials objSigningCredentials = new SigningCredentials(objSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken objJwtSecurityToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: arrClaims,
                expires: DateTime.UtcNow.AddHours(expirationHours),
                signingCredentials: objSigningCredentials
            );

            // Create the JWT token
            JwtSecurityTokenHandler objJwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // Return the serialized JWT token as a string
            return new { Token = objJwtSecurityTokenHandler.WriteToken(objJwtSecurityToken) };
        }
    }
}
