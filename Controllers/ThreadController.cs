using Data;
using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
[Route("api/thread")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThreadController: ControllerBase
{
    private ILogger _logger;

    public ThreadController(ILogger<ThreadController> logger)
    {
        _logger = logger;
    }

    [Route("{id:int}",Name = "getThread")]
    [HttpGet]
    public ActionResult Get(int id)
    {
        using DataContext db = new();
        try
        {
            Entity.Thread thread =  db.Threads.AsParallel().Where(thread => thread.ID == id).First();
            return Ok(thread);
        }
        catch
        {
            return BadRequest("Wasnt able to find the thread");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Entity.Thread thread)
    {   
        using DataContext db = new();
        try
        {
            int collID =  db.Collections.AsParallel().Where(collection => collection.ColName == thread.CollectionName).First().ID;
            thread.CollectionID = collID;

            await db.AddAsync(thread);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}