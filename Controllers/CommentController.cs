using Data;
using Entity;
using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
[Route("api/comment")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CommentController:ControllerBase
{
    private ILogger _logger;

    public CommentController(ILogger<CommentController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> Post(Comment comment)
    {   
        DataContext db = new();
        try
        {
            await db.AddAsync(comment);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}