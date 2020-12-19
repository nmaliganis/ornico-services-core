using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ornico.test.functional.Base
{
  public static class JwtTokenHelper
  {
    public static string GenerateEmailVerificationLink(string user)
    {
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

      return GenerateJwtToken(config, user);
    }

    private static string GenerateJwtToken(IConfigurationRoot config,  string registeredUser)
    {
      List<Claim> claims = new List<Claim>
      {
        new Claim(ClaimTypes.Email, registeredUser),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(config.GetSection("TokenAuthentication:SecretKey").Value);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(int.Parse(config
          .GetSection("TokenAuthentication:ExpirationTimeInMinutes").Value)),
        Issuer = config.GetSection("TokenAuthentication:Issuer").Value,
        Audience = config.GetSection("TokenAuthentication:Audience").Value,
        SigningCredentials =
          new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      string tokenValue = tokenHandler.WriteToken(token);
      return tokenValue;
    }
  }
}
