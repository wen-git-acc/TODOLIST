using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoList.Service.Services.JWTRepository
{
    public class JWTManagerRepository : IJWTManagerRepository

    {
        Dictionary<string, string> UsersRecords = new ()
        {
            { "user1","password1"},
            { "user2","password2"},
            { "user3","password3"},
        };

        private readonly IConfiguration _configuration;
        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens? Authenticate(Users users)
        {
            if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
            {
                return null;
            }

            return GenerateAccessToken(users);
        }

        public Tokens GenerateAccessToken(Users users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var roles = users.Name == "user2" ? "notuser" : "user";
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            List<Claim> claims = new()
            {
                new(ClaimTypes.Name, users.Name),
                new(ClaimTypes.Role, roles)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }

        public string GenerateRefreshToken()
        {
            return "hi";
        }
    }
}
