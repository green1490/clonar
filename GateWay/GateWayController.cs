using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
[Route("")]
public class GateWayController : ControllerBase
{
    [HttpPost]
    [Route("[action]")]
    public ActionResult Test()
    {
        return Ok(new {Response = "ok"});
    }
}