using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FieldGroove.Api.Services
{
	public class JwtToken(IConfiguration configuration)
    {
		private readonly IConfiguration configuration = configuration;

        public string GenerateJwtToken(string username)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, username)]),
				Expires = DateTime.Now.AddMinutes(60),
				Issuer = configuration["Jwt:Issuer"],
				Audience = configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
