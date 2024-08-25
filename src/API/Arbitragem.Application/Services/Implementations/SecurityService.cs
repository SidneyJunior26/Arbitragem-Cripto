using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Solution.Core.Entities;
using Solution.Core.Enum;

namespace Arbitragem.Application.Services.Implementations;

public class SecurityService : ISecurityService
{
    private readonly IConfiguration _config;

    public SecurityService(IConfiguration config)
    {
        _config = config;
    }

    public string Login(User user, LoginInputModel loginInputModel)
    {
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("name", user.Name)
        });

        if (user.Level == Level.ADM)
            subject.AddClaim(new Claim("adm", ""));
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var key = Encoding.ASCII.GetBytes(_config["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescription = new SecurityTokenDescriptor {
            Subject = subject,
            Audience = _config["JwtBearerTokenSettings:Audience"],
            Issuer = _config["JwtBearerTokenSettings:Issuer"],
            Expires = DateTime.UtcNow.AddDays(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
        
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }

    public bool ValidatePassword(User user, string password)
    {
        if (user.Password == Encrypt(password))
            return true;
        else
            return false;
    }
    
    public string Encrypt(string input) {
        using (MD5 md5 = MD5.Create()) {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++) {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}