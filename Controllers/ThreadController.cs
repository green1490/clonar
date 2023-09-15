using Data;
using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
[Route("api/thread")]
[ApiExplorerSettings(IgnoreApi = true)]
public class ThreadController: ControllerBase
{
    private readonly ILogger _logger;
    private readonly DataContext _db;

    public ThreadController(ILogger<CollectionController> logger,DataContext db)
    {
        _logger = logger;
        _db = db;
    }

    [Route("{id:int}",Name = "getThread")]
    [HttpGet]
    public ActionResult Get(int id)
    {
        try
        {
            Entity.Thread thread =  _db.Threads.AsParallel().Where(thread => thread.ID == id).First();
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
        try
        {
            int collID =  _db.Collections.AsParallel().Where(collection => collection.ColName == thread.CollectionName).First().ID;
            thread.CollectionID = collID;

            await _db.AddAsync(thread);
            await _db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}