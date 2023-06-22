using DAL.Data;
using DAL.Models;
//using DAL_TestDB.Models;
//using DAL_TestDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Configuration;

public class Tokenizer
{
    private readonly IConfiguration _configuration;
    private readonly DatabaseContext _context;
    private readonly JwtSettings _jwtSettings;

    public Tokenizer(IConfiguration configuration, DatabaseContext context, JwtSettings jwtSettings)
    {
        _configuration = configuration;
        _context = context;
        _jwtSettings = jwtSettings;
    }
    public string GenerateToken(string appKey, int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, appKey),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpirationMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // Store the token in the AppSessions table
        var appSession = new AppSession
        {
            Token = tokenString,
            Date = DateTime.UtcNow,
            ProjectApplicationId = GetProjectApplicationId(appKey, userId),
            UserId = userId
        };

        _context.AppSessions.Add(appSession);
        _context.SaveChanges();

        return tokenString;
    }

    public bool ValidateToken(string token, out string errorMessage)
    {
        errorMessage = null;

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken validatedToken;
            tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            if (validatedToken.ValidTo < DateTime.UtcNow)
            {
                errorMessage = "Token has expired";
                return false;
            }

            var appSession = _context.AppSessions.FirstOrDefault(a => a.Token == token);
            if (appSession == null)
            {
                errorMessage = "Invalid token";
                return false;
            }

            // Optionally, you can check if the token belongs to the correct user/application

            return true;
        }
        catch (SecurityTokenExpiredException)
        {
            errorMessage = "Token has expired";
            return false;
        }
        catch
        {
            errorMessage = "Invalid token";
            return false;
        }
    }

    private int GetProjectApplicationId(string appKey, int userId)
    {
        var appSession = _context.AppSessions.FirstOrDefault(session =>
            session.ProjectApplication.AppKey == appKey && session.UserId == userId);

        return appSession?.ProjectApplicationId ?? 0;
    }

}

