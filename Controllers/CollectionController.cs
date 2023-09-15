using Microsoft.AspNetCore.Mvc;
using Entity;
using Data;

[ApiController]
[Route("api/collection")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CollectionController:ControllerBase
{
    private readonly ILogger _logger;
    private readonly DataContext _db;

    public CollectionController(ILogger<CollectionController> logger,DataContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Collection collection)
    {
        try 
        {
            await _db.AddAsync(collection);
            await _db.SaveChangesAsync();
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
        try
        {
            Collection collection = _db.Collections.Where(x => x.ColName == name).First();
            Entity.Thread[] threads = _db.Threads.AsParallel().Where(thread => thread.CollectionID == collection.ID).ToArray();
            return Ok(threads);
        }
        catch
        {
            return BadRequest();
        }
    }
}