using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Entity;

namespace clonar.Controllers;

[ApiController]
public class GateWayController : ControllerBase
{
    private readonly ILogger _logger;
    private HttpClient client = new () {BaseAddress = new Uri("http://localhost:5246") };


    public GateWayController(ILogger<GateWayController> logger)
    {
        _logger = logger;
    }

    [HttpGet("login")]
    public ActionResult Get([FromQuery] Login login)
    {
        return RedirectToAction("Get","User",login);
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {
        using var response = await client.PostAsJsonAsync("/api/user/registration", account);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("creation")]
    public ActionResult Post([FromBody] Collection collection)
    {
        return Ok();
    }

    [HttpGet("logout")]
    public async Task<ActionResult> Get()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return Ok();
    }
}