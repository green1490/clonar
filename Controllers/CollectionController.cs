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
}