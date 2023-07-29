using Data;
using Entity;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace clonar.Controllers;

[ApiController]
[Route("api/user")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("login")]
    public async Task<ActionResult> Get([FromQuery] Login login)
    {
        using DataContext db = new();
        var users = db.Accounts
            .Where(x => x.Email == login.Email && x.Password == login.Password);
        if (users.Count() != 1 || users is null)
        {
            return BadRequest(JsonSerializer.Serialize(new {Response = "Wrong email or password"}));
        }
        var user =  users.First();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("id",Convert.ToString(user.ID))

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

    [HttpPost("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {   
        DataContext db = new();
        db.Add(account);
        try 
        {
            await db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            _logger.LogCritical("User already exits");
            return BadRequest(JsonSerializer.Serialize(new {Response = "Unseccessful registration"}));
        }
    }
}