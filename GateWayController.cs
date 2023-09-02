using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace clonar.Controllers;

[Authorize]
[ApiController]
public class GateWayController : ControllerBase
{
    private readonly ILogger _logger;
    private HttpClient client = new () {BaseAddress = new Uri("http://localhost:5246") };

    public GateWayController(ILogger<GateWayController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public ActionResult Get([FromQuery] Login login)
    {
        return RedirectToAction("Get","User",login);
    }

    [AllowAnonymous]
    [HttpPost("registration")]
    public async Task<ActionResult> Post([FromBody] Account account)
    {
        using var registrationRespone = await client.PostAsJsonAsync("/api/user/registration", account);
        Karma karma = new() {UserID = account.ID};
        
        using var karmaResponse = await client.PostAsJsonAsync("api/karma", karma);

        if (registrationRespone.IsSuccessStatusCode && karmaResponse.IsSuccessStatusCode)
        {
            return Ok();
        }
        return BadRequest();
    }


    [HttpPost("thread")]
    public async Task<ActionResult> CreateThread([FromBody] Entity.Thread thread)
    {
        int id = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value);
        thread.UserID = id;

        using var response = await client.PostAsJsonAsync("api/thread",thread);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("thread")]
    public ActionResult GetThread(int id)
    {
        return RedirectToRoute("getThread", new {id = id});
    }

    [HttpGet("logout")]
    public async Task<ActionResult> Get()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return Ok();
    }

    [HttpPost("collection")]
    public async Task<ActionResult> CreateCollection([FromBody] Collection collection)
    {
        int id = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value);
        collection.OwnerID = id;

        var result = await client.PostAsJsonAsync("api/collection",collection);
        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("collection/{name}")]
    public ActionResult GetCollection(string name)
    {
        return RedirectToRoute("listThreads", new {name = name});
    }

    [HttpPost("comment")]
    public ActionResult CreateComment([FromBody] Comment comment)
    {
        int id = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value);
        comment.UserID = id;
        client.PostAsJsonAsync("api/comment",comment);
        return Ok();
    }

    [HttpGet("comment")]
    public ActionResult GetComment(int id)
    {
        return RedirectToRoute("getComments", new {id = id});
    }

    [HttpPost("vote/{value:int}")]
    public ActionResult Post([FromBody] Vote vote) 
    {
        return Ok();
    }

}