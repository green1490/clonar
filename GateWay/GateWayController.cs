using Microsoft.AspNetCore.Mvc;

namespace clonar.Controllers;

[ApiController]
[Route("[controller]")]
public class GateWayController : ControllerBase
{
    [HttpGet(Name = "Test")]
    public ActionResult GetTest()
    {
        return Ok(new {Response = "ok"});
    }
}