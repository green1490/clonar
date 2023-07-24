using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
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

    [HttpPost("create")]
    public ActionResult Post()
    {
        return Ok();
    }
}