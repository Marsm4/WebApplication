//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("error")]
//public class ErrorController : ControllerBase
//{
//    [HttpGet]
//    public IActionResult HandleError()
//    {
//        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
//        var exception = context?.Error;

//        return Problem(
//            title: "Internal Server Error",
//            detail: exception?.Message ?? "An unexpected error occurred.",
//            statusCode: StatusCodes.Status500InternalServerError
//        );
//    }
//}
