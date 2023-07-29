using Data;
using Entity;
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

    [HttpPost("creation")]
    public async Task<ActionResult> Post([FromBody] Entity.Thread thread)
    {   
        using DataContext db = new();
        try
        {
            await db.AddAsync(thread);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
        {
            _logger.LogCritical(e.ToString());
            return BadRequest();
        }
    }
}