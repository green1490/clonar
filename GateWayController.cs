using Microsoft.AspNetCore.Mvc;
using Entity;
using System.Text.Json;

namespace clonar.Controllers;

[ApiController]
[Route("")]
public class GateWayController : ControllerBase
{
    private readonly ILogger _logger;
    private HttpClient client = new () {BaseAddress = new Uri("http://localhost:5246") };


    public GateWayController(ILogger<GateWayController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("login")]
    public ActionResult Get([FromQuery] Login login)
    {
        return RedirectToAction("Get","User",login);
    }

    [HttpPost]
    [Route("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {
        using var response = await client.PostAsJsonAsync("/api/user/registration", account);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        return BadRequest();
    }
}