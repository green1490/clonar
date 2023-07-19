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
    public ActionResult Get([FromQuery] Login login)
    {
        using UserContext db = new();
        var user = db.Accounts
            .Where(x => x.email == login.Email && x.password == login.Password);
        if (user.Count() != 1 || user is null)
        {
            return BadRequest(JsonSerializer.Serialize(new {Response = "Wrong email or password"}));
        }
        return Ok(JsonSerializer.Serialize(new {Response = user.First()}));
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