using System.Security.Claims;
using Data;
using Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MinAPISeparateFile;

public static class UserEndPoint
{
    public static void MapUser(this WebApplication app)
    {
        app.MapGet("/logout", async ctx =>
        {
            await ctx.SignOutAsync();
        })
        .WithOpenApi();

        app.MapGet("/login", async ([AsParameters] Login login,DataContext db,HttpContext ctx) => 
        {
            var user = db.Accounts.First(x => x.Email == login.Email && x.Password == login.Password);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new("id",Convert.ToString(user.ID))
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme
            );

            await ctx.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new(claimsIdentity)
            );
            
            await ctx.Response.WriteAsJsonAsync(user);
        })
        .WithOpenApi();

        app.MapPost("/registration", async (Account account,DataContext db) =>
        {
            await db.AddAsync(account);
            await db.SaveChangesAsync();
        })
        .WithOpenApi();
    }
}