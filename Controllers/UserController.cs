using Data;
using Entity;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace clonar.Controllers;

[ApiController]
[Route("api/user")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UserController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly DataContext _db;

    public UserController(ILogger<CollectionController> logger,DataContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet("login")]
    public async Task<ActionResult> Get([FromQuery] Login login)
    {
        var users = _db.Accounts
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
        try 
        {
            await _db.AddAsync(account);
            await _db.SaveChangesAsync();
            return Ok();
        }
        catch(DbUpdateException err)
        {
            _logger.LogCritical(err.ToString());
            _logger.LogCritical("User already exits");
            return BadRequest(JsonSerializer.Serialize(new {Response = "Unseccessful registration"}));
        }
    }
}