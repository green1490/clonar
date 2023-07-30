using Microsoft.AspNetCore.Mvc;
using Entity;
using Data;

[ApiController]
[Route("api/collection")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CollectionController:ControllerBase
{
    private ILogger _logger;

    public CollectionController(ILogger<CollectionController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Collection collection)
    {
        DataContext db = new();
        try 
        {
            await db.AddAsync(collection);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{name}", Name = "listThreads")]
    public ActionResult Get(string name)
    {   
        DataContext db = new();

        try
        {
            Collection collection = db.Collections.Where(x => x.ColName == name).First();
            Entity.Thread[] threads = db.Threads.AsParallel().Where(thread => thread.CollectionID == collection.ID).ToArray();
            return Ok(threads);
        }
        catch
        {
            return BadRequest();
        }
    }
}