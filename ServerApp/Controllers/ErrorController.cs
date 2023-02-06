using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using System.Net;

namespace ServerApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exeptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exeptionHandlerFeature.Error; 
            ErrorResponse errorResponse = new ErrorResponse
            {
                Details = exception.StackTrace,
                Title = exception.Message
            };

            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
            return result;
        }
    }
}
