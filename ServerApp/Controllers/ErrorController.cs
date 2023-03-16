using System.Net;

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
        ErrorResponse errorResponse = new ErrorResponse
        {
            Message = exception?.Message ?? "Unknown exception occured"
        };
        var result = new ObjectResult(errorResponse)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
        };
        return result;
    }
}
