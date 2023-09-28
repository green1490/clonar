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

    // [HttpPost("thread")]
    // public async Task<ActionResult> CreateThread([FromBody] Entity.Thread thread)
    // {
    //     int id = Convert.ToInt32(User.Claims.First(x => x.Type == "id").Value);
    //     thread.UserID = id;

    //     using var response = await client.PostAsJsonAsync("api/thread",thread);
    //     if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //     {
    //         return Ok();
    //     }
    //     return BadRequest();
    // }

    // [HttpGet("thread")]
    // public ActionResult GetThread(int id)
    // {
    //     return RedirectToRoute("getThread", new {id = id});
    // }


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