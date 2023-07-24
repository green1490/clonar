using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Data;
using System.Text.Json;

namespace clonar.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("login")]
    public async Task<ActionResult> Get([FromQuery] Login login)
    {
        using UserContext db = new();
        var users = db.Accounts
            .Where(x => x.email == login.Email && x.password == login.Password);
        if (users.Count() != 1 || users is null)
        {
            return BadRequest(JsonSerializer.Serialize(new {Response = "Wrong email or password"}));
        }
        var user =  users.First();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim("id",Convert.ToString(user.id))

        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme
        );
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity)
        );
        return Ok(JsonSerializer.Serialize(new {Response = user }));
    }

    [HttpPost]
    [Route("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {   
        UserContext db = new();
        db.Add(account);
        try 
        {
            await db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return BadRequest(JsonSerializer.Serialize(new {Response = "Unseccessful registration"}));
        }
    }
}