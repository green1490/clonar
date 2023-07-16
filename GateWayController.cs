using Microsoft.AspNetCore.Mvc;
using Entity;
using System.Text.Json;

namespace clonar.Controllers;

[ApiController]
[Route("")]
public class GateWayController : ControllerBase
{
    private readonly ILogger _logger;

    public GateWayController(ILogger<GateWayController> logger)
    {
        _logger = logger;
    }

    private HttpClient client = new () {BaseAddress = new Uri("http://localhost:5246") };
    [HttpGet]
    [Route("login")]
    public async Task<ActionResult> Get([FromBody] Login login)
    {
        using var result = await client.PostAsync(
            "/api/user/login",
            new StringContent(JsonSerializer.Serialize(login))
        );
        return Ok(result.Content);
    }

    [HttpPost]
    [Route("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {
        using var response = await client.PostAsJsonAsync("/api/user/registration", account);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation("Successful registration");
            return Ok();
        }
        _logger.LogInformation(await response.Content.ReadAsStringAsync());
        return BadRequest();
    }
}