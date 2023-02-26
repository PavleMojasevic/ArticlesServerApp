using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        var exeptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exeptionHandlerFeature?.Error;
        return BadRequest(exception.Message);
    }
}
