using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public ActionResult Get([FromBody] Login login)
    {
        using UserContext db = new();
        var user = db.Accounts
            .Where(x => x.email == login.UserName && x.password == login.Password);
        if (user is null)
        {
            return BadRequest(JsonSerializer.Serialize(new {Response = "Wrong email or password"}));
        }
        return Ok(JsonSerializer.Serialize(new {Response = user}));
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
            _logger.LogError("Internal error");
            return BadRequest(JsonSerializer.Serialize(new {Response = "Unseccessful registration"}));
        }
    }
}