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

    [HttpGet("{id:int}",Name = "getComments")]
    public ActionResult Get(int id)
    {
        using DataContext db = new();
        try 
        {
            Comment[] comments = db.comments.AsParallel().Where(comment => comment.threadID == id).ToArray();
            return Ok(comments);
        }
        catch
        {
            return BadRequest("Wasnt able to find any comment");
        }
    }
}