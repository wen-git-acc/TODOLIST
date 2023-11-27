using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Rpc.Context;
using Microsoft.IdentityModel.Tokens;

namespace TodoList.Service.Services.JWTRepository
{
    public class JWTManagerRepository : IJWTManagerRepository

    {
        private JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();


        Dictionary<string, string> UsersRecords = new ()
        {
            { "liangfang","liangfang"},
            { "user2","password2"},
            { "xiao","xiao"},
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
            var roles = users.Name == "user2" ? "notuser" : "user";
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new()
            {
                new(ClaimTypes.Name, users.Name),
                new(ClaimTypes.Role, roles),
                new (ClaimTypes.Email, users.Name)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredential
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }

        public string ReadClaims(string token, string targetClaimType)
        {
            var targetValue = "";
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }

            if (String.IsNullOrEmpty(token))
            {
                return targetValue;
            }

            var jwt = tokenHandler.ReadToken(token) as JwtSecurityToken;
            targetValue = jwt.Claims.First(claim => claim.Type == targetClaimType.ToLower()).Value ?? "";
            return targetValue;

        }
      
    }
}
