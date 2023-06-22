using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using DAL.Models;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Services
{

    public class BasicAuthenticationAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly DatabaseContext _context;

        public BasicAuthenticationAttribute(DatabaseContext context)
        {
            _context = context;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //var appKey = _configuration.GetValue<string>("AppSettings:AppKey");
            //var appValue = _configuration.GetValue<string>("AppSettings:AppValue");

            //var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{appKey}:{appValue}"));

            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Basic "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Substring(6)));
            string[] usernamePassword = credentials.Split(':');
            string appKey = usernamePassword[0];
            string token = usernamePassword[1];
            int userId = int.Parse(usernamePassword[2]);

            // Example: Query the AppSession table to check if the Token and UserId match
            AppSession appSession = await _context.AppSessions.FirstOrDefaultAsync(session =>
                session.Token == token && session.UserId == userId && session.ProjectApplication.AppKey == appKey);
            if (appSession == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // If the authentication is successful, you can update the session date
            appSession.Date = DateTime.Now;
            await _context.SaveChangesAsync();

            // Set the Token header in the response
            context.HttpContext.Response.Headers.Add("Token", appSession.Token);
        }
    }
}
