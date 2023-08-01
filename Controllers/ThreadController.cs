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

    [Route("view/{collection}")]
    [HttpGet]
    public ActionResult Get(string collection)
    {
        return Ok();
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